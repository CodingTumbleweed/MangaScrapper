using MangaScrapper.Core.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MangaScrapper.Core.HTTP
{
    class DownloadAsync : IDownload
    {

        /// <returns>Html Document String</returns>
        public async Task<string> LoadDocumentAsync(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri [LoadDocumentAsync]");

            try
            {
                using (WebClient client = new WebClient())
                {
                    var html = await client.DownloadStringTaskAsync(uri);
                    return html;
                }
            }
            catch (WebException ex)
            {
                Log.Error("LoadDocumentAsync threw Webexception for url: " + uri, ex);
                throw;
            }
        }

        /// <summary>
        /// Saves Image asynchronously
        /// </summary>
        public async Task SaveImgAsync(Uri imageUri, string fileName)
        {
            if (imageUri == null)
                throw new ArgumentNullException("imageUri [SaveImgAsync]");
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName [SaveImgAsync]");

            //In case if file name is given with no file extension,
            //get extension from 'imageUri' and append to 'fileName'
            if (!Path.HasExtension(fileName))
                fileName +=  imageUri.GetFileExtension();

            try
            {
                using (WebClient client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(imageUri, fileName);
                }
            }
            catch (WebException ex)
            {
                Log.Error("SaveImageAsync threw Webexception for url: " + imageUri, ex);
                throw;
            }
        }        
    }
}
