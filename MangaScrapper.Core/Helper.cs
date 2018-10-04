using System;

namespace MangaScrapper.Core
{
    static class Helper
    {
        public static bool IsUrlValid(string url)
        {
            Uri uriResult;
            
            bool flag = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return flag;
        }
    }
}
