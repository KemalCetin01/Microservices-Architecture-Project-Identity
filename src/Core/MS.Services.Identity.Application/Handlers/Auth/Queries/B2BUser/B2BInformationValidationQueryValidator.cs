using FluentValidation;
using MS.Services.Identity.Application.Handlers.Auth.Queries;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser;

public class B2BInformationValidationQueryValidator : AbstractValidator<B2BValidateSignUpInfoQuery>
{
    public B2BInformationValidationQueryValidator()
    {
        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering company information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.SectorId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.NumberOfEmployeeId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.ActivityAreaId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering First Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(256)
            .WithMessage("It cannot be saved without entering Last Name information. And it must be a maximum of 256 characters.");

        RuleFor(x => x.Suffix).NotEmpty().MaximumLength(10)
            .WithMessage("It cannot be saved without entering Suffix information. And it must be a maximum of 10 characters.");

        RuleFor(x => x.PositionId).NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.OccupationId).NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
