using FluentValidation;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CMoreQuestionsCommandValidator : AbstractValidator<B2CMoreQuestionsCommand>
{
    public B2CMoreQuestionsCommandValidator()
    {
        RuleFor(x => x.SectorId).NotEmpty()
           .GreaterThanOrEqualTo(1);
        RuleFor(x => x.ActivityAreaId).NotEmpty()
          .GreaterThanOrEqualTo(1);
        RuleFor(x => x.OccupationId).NotEmpty()
          .GreaterThanOrEqualTo(1);
    }
}
