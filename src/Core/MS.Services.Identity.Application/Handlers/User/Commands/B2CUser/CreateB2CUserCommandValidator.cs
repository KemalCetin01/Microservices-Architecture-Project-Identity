using FluentValidation;
using MS.Services.Identity.Domain.Enums;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2CUser;

public class CreateB2CUserCommandValidator : AbstractValidator<CreateB2CUserCommand>
{
    public CreateB2CUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(256)
              .WithMessage("It cannot be saved without entering First Name information. And it must be a maximum of 256 characters.");
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Last Name information. And it must be a maximum of 256 characters.");
        RuleFor(x => x.Password).MinimumLength(6);
        RuleFor(x => x.RePassword).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Password).NotEmpty()
            .Equal(x => x.RePassword).WithMessage("Passwords should be same");
        RuleFor(x => x.CountryId).NotEmpty()
            .GreaterThanOrEqualTo(1);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneCountryCode).NotEmpty().MaximumLength(8)
            .WithMessage("it must be a maximum of 15 characters."); ;
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15)
            .WithMessage("it must be a maximum of 15 characters.");
        RuleFor(x => x.SiteStatus).NotEmpty()
            .Must(ValidateSiteStatus).WithMessage("Invalid site status") ;
        RuleFor(x => x.UserStatus).NotEmpty()
            .Must(ValidateUserStatus).WithMessage("Invalid user status") ;
    }

    private bool ValidateSiteStatus(SiteStatusEnum siteStatus)
    {
        if (siteStatus == SiteStatusOpen || siteStatus == SiteStatusClosed)
            return true;
        return false;
    }
    private bool ValidateUserStatus(UserStatusEnum userStatus)
    {
        if (userStatus == UserStatusDeleted || userStatus == UserStatusActive || userStatus == UserStatusInactive)
            return true;
        return false;
    }
}
