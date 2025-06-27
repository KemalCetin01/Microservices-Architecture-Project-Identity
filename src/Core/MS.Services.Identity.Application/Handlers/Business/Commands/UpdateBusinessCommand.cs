using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.EntityConstants;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public sealed class UpdateBusinessCommand : UpdateBusinessRequestDto, ICommand<GetBusinessResponseDto>
{
}
public sealed class BusinessCommandHandler : BaseCommandHandler<UpdateBusinessCommand, GetBusinessResponseDto>
{
    private readonly IBusinessService _businessService;
    
    public BusinessCommandHandler(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    public override async Task<GetBusinessResponseDto> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        return await _businessService.UpdateAsync(request, cancellationToken);
    }
}

public class UpdateBusinessCommandValidator : AbstractValidator<UpdateBusinessCommand>
{
    public UpdateBusinessCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(BusinessConstants.NameMaxLength).NotEmpty();
        RuleFor(x => x.Phone).MaximumLength(BusinessConstants.PhoneMaxLength);
        RuleFor(x => x.PhoneCountryCode).MaximumLength(BusinessConstants.PhoneCountryCodeMaxLength);
        RuleFor(x => x.FaxNumber).MaximumLength(BusinessConstants.FaxNumberMaxLength);
        
        RuleFor(x => x.BusinessStatus).NotEmpty();
        RuleFor(x => x.DiscountRate).InclusiveBetween(0, 100)
            .WithMessage("The value must be between 0 and 100.");
    }
}
