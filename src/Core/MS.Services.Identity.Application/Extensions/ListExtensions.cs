using System.Collections;

namespace MS.Services.Identity.Application.Extensions;

public static class ListExtensions
{
    
    public static bool IsNullOrEmpty<T>(this List<T>? list)
    {
        return list == null || list.Count == 0;
    }

}