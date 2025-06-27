using FluentValidation;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;

public class ResetB2BUserPasswordCommandValidator : AbstractValidator<ResetB2BUserPasswordCommand>
{
    public ResetB2BUserPasswordCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
             .WithMessage("it must be a minimum of 6 characters.");
        RuleFor(x => x.RePassword).NotEmpty().MinimumLength(6)
             .WithMessage("it must be a minimum of 6 characters.");
        RuleFor(x => x.Password).Equal(x=>x.RePassword).WithMessage("Passwords1 should be same");
       

    }


}
