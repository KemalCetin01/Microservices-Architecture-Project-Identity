using FluentValidation;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2CUser;

public class ResetB2CUserPasswordCommandValidator : AbstractValidator<ResetB2CUserPasswordCommand>
{
    public ResetB2CUserPasswordCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
             .WithMessage("it must be a minimum of 6 characters.");
        RuleFor(x => x.RePassword).NotEmpty().MinimumLength(6)
             .WithMessage("it must be a minimum of 6 characters.");
        RuleFor(x => x.Password).Equal(x=>x.RePassword).WithMessage("Passwords should be same");
       

    }


}
