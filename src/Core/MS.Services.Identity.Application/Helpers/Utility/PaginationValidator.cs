using FluentValidation;
using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Helpers.Utility;

public class PaginationValidator : AbstractValidator<Pagination>
{
    public PaginationValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).When(x => x != null);

        RuleFor(x => x.CurrentPage)
            .GreaterThanOrEqualTo(1);
    }
}