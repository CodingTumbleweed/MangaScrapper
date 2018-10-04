using System;
using System.Threading.Tasks;

namespace MangaScrapper.Core.HTTP
{
    interface IDownload
    {
        Task<string> LoadDocumentAsync(Uri uri);
        Task SaveImgAsync(Uri imageUri, string fileName);
    }
}
