using FluentValidation;

namespace MS.Services.Identity.Application.Helpers.Utility;

public static class FluentValidationHelper
{

    #region Custom Rules

    public static IRuleBuilderOptions<T, IEnumerable<TSource>?> Unique<T, TSource, TResult>(
        this IRuleBuilder<T, IEnumerable<TSource>?> ruleBuilder,
        Func<TSource, TResult> selector, string? message = null)
    {
        return ruleBuilder
            .Must(x => x.IsDistinct(selector))
            .WithMessage(message ?? "Aynı öğe birden fazla kez eklenemez.");
    }

    #endregion


    #region Private Methods

    private static bool IsDistinct<TSource, TResult>(this IEnumerable<TSource>? elements, Func<TSource, TResult> selector)
    {
        if (elements==null) return true;
        var hashSet = new HashSet<TResult>();
        foreach (var element in elements.Select(selector))
            if (!hashSet.Contains(element))
                hashSet.Add(element);
            else
                return false;
        return true;
    }

    #endregion
}