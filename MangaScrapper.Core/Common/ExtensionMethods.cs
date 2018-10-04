using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class ExtensionMethods
{
    public static bool HasItems<T>(this IEnumerable<T> data)
    {
        return data != null && data.Any();
    }

    public static string GetFileExtension(this Uri url)
    {
        return Path.GetExtension(url.OriginalString);
    }
}

