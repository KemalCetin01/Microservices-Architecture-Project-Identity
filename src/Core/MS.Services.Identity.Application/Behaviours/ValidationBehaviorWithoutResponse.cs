using System.Collections.Immutable;
using FluentValidation;
using MediatR;
using MS.Services.Core.Base.Extentions;
using MS.Services.Core.ExceptionHandling.Wrapper;
using ValidationException = MS.Services.Core.ExceptionHandling.Exceptions.ValidationException;

namespace MS.Services.Identity.Application.Behaviours;

/// <summary>
///     Pipeline behavior that validates requests using a collection of validators
/// </summary>
/// <typeparam name="TRequest">The type of request to validate</typeparam>
/// <typeparam name="TResponse">
///     <see cref="Unit" />
/// </typeparam>
public class ValidationBehaviorWithoutResponse<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviorWithoutResponse(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName.ToCamelCase(),
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .SelectMany(errorItem => errorItem.Values.Select(errorMessage => new ExceptionErrorResponse
            {
                Field = errorItem.Key,
                Message = errorMessage
            }))
            .ToImmutableList();

        if (errors.Any())
            throw new ValidationException(errors);

        return await next();
    }
}