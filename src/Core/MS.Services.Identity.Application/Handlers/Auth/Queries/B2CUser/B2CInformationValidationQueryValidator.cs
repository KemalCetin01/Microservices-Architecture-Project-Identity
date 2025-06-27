using FluentValidation;
using MS.Services.Identity.Application.Handlers.Auth.Queries;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser;

public class B2CInformationValidationQueryValidator : AbstractValidator<B2CValidateSignUpInfoQuery>
{
    public B2CInformationValidationQueryValidator()
    {

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering First Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Last Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.Suffix).NotEmpty().MaximumLength(10)
            .WithMessage("It cannot be saved without entering Suffix information. And it must be a maximum of 10 characters.");
    }
}
