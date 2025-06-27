using System.Linq.Expressions;
using System.Reflection;
using FluentValidation.Internal;
using MS.Services.Core.Base.Extentions;

namespace MS.Services.Identity.Application.Helpers.Utility;

public static class CamelCasePropertyNameResolver
{
    public static string ResolvePropertyName(Type type, MemberInfo memberInfo, LambdaExpression expression)
    {
        return (DefaultPropertyNameResolver(memberInfo, expression) ?? string.Empty).ToCamelCase();
    }

    private static string? DefaultPropertyNameResolver(MemberInfo? memberInfo, LambdaExpression? expression)
    {
        if (expression == null) return memberInfo != null ? memberInfo.Name : null;
        var chain = PropertyChain.FromExpression(expression);
        if (chain.Count > 0) return chain.ToString();

        return memberInfo != null ? memberInfo.Name : null;
    }
}