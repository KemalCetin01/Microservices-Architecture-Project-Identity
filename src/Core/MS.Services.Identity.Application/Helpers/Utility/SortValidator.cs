using FluentValidation;
using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Helpers.Utility;

public class SortValidator : AbstractValidator<Sort>
{
    public SortValidator()
    {
        RuleFor(x => x.Direction)
            .NotEmpty()
            .Must(x => new[] {"ASC", "DESC"}.Contains(x, StringComparer.CurrentCultureIgnoreCase));

        RuleFor(x => x.Field)
            .NotEmpty();
    }
}