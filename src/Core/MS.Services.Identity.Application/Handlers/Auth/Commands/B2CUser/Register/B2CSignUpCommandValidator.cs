using FluentValidation;
using MS.Services.Identity.Domain.Enums;
using System.Linq;
using System.Text.RegularExpressions;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CSignUpCommandValidator : AbstractValidator<B2CSignUpCommand>
{
    public B2CSignUpCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering First Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Last Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.Suffix).NotEmpty().MaximumLength(10)
            .WithMessage("It cannot be saved without entering Suffix information. And it must be a maximum of 10 characters.");

        RuleFor(x => x.CountryId).NotEmpty()
           .GreaterThanOrEqualTo(1);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneCountryCode).NotEmpty().MaximumLength(8)
            .WithMessage("it must be a maximum of 15 characters."); ;
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15)
            .WithMessage("it must be a maximum of 15 characters.");
        RuleFor(x => x.VerificationType).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
             .WithMessage("it must be a minimum of 6 characters.");
        RuleFor(x => x.Password).Equal(x=>x.RePassword).WithMessage("Passwords should be same");
        RuleFor(x => x.ConfirmRegisters)
            .NotEmpty().WithMessage("At least one confirm register type must be selected.")
            .Must(ContainExplicitConsent).WithMessage("Explicit consent must be selected.");

    }

    private bool ContainExplicitConsent(List<ConfirmRegisterEnum> confirmRegisters)
    {
        return confirmRegisters != null &&
           confirmRegisters.Contains(ConfirmRegisterEnum.ExplicitConsent) &&
           confirmRegisters.Contains(ConfirmRegisterEnum.KVKKConsent) &&
           confirmRegisters.Count >= 2;
    }

}
