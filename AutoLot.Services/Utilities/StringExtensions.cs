using System;

namespace AutoLot.Services.Utilities;

public static class StringExtensions
{
    public static string RemoveControllersSuffix(this string original)
    {
        return original.Replace("Controllers","",StringComparison.OrdinalIgnoreCase);
    }

    public static string RemoveAsyncSuffix(this string original)
    {
        return original.Replace("Async","",StringComparison.OrdinalIgnoreCase);
    }
    public static string RemovePageModelSuffix(this string original)
    {
        return original.Replace("PageModel","",StringComparison.OrdinalIgnoreCase);
    }
}
