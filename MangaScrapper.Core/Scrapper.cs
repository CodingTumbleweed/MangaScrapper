using MangaScrapper.Core.Configuration;
using MangaScrapper.Core.HTTP;
using MangaScrapper.Core.Model.DataModel;
using MangaScrapper.Core.Model.Enum;
using MangaScrapper.Core.Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaScrapper.Core
{
    /// <summary>
    /// A Facade for Core Library
    /// </summary>
    class Scrapper
    {
        private IParseHtml _parseHtml;
        private IDownload _download;
        private ConfigurationModel Selected;

        public Scrapper()
            : this(new ParseHtml(), new DownloadAsync())
        {
        }

        internal Scrapper(IParseHtml parseHtml, IDownload download)
        {
            _parseHtml = parseHtml;
            _download = download;
            Selected = DomainSettings.SelectedConfiguration;
            if (Selected == null)
                throw new MangaScrapperException("Error In Configuration Selection");
        }

        public async Task<IEnumerable<BaseModel>> GetSeriesList()
        {
            Uri AllSeriesUrl = new Uri(Selected.AllSeriesUrl);
            //Download Html document
            string doc = await _download.LoadDocumentAsync(AllSeriesUrl).ConfigureAwait(false);
            //Parse the Html document to get List of Series
            return _parseHtml.GetList(doc, BaseType.Serie);
        }

        /// <param name="SeriePageUrl">Serie Page URL Listing All Chapters of Serie</param>
        public async Task<IEnumerable<BaseModel>> GetChapterList(Uri SeriePageUrl)
        {
            //Download Html document
            string doc = await _download.LoadDocumentAsync(SeriePageUrl).ConfigureAwait(false);
            //Parse the Html document to get List of Chapters
            return _parseHtml.GetList(doc, BaseType.Chapter);
        }


        /// <param name="ChapterStartUrl">URL for First Page of Chapter</param>
        /// <param name="NextChapterUrl">URL for First Page of Next Chapter</param>
        /// <param name="FilePath">Path to save Chapter</param>
        public async Task DownloadFullChapter(Uri ChapterStartUrl, string NextChapterUrl, string FilePath)
        {
            Uri NextLinkUrl = ChapterStartUrl;
            bool LoopAgain = true;
            bool HasNextChapterUrl = !string.IsNullOrWhiteSpace(NextChapterUrl);
            int ChapterPageNo = 1;
            do
            {
                //Download Html document
                string doc = await _download.LoadDocumentAsync(NextLinkUrl).ConfigureAwait(false);
                //Parse the Html document to get List of Series
                var result = _parseHtml.GetChapterLinks(doc);
                //Download Image
                Uri ImageUrl = new Uri(result.ImageUrl);
                await _download.SaveImgAsync(ImageUrl, Path.Combine(FilePath, ChapterPageNo.ToString()));

                if (HasNextChapterUrl)
                {
                    /***
                     * If No Next is found OR Next Url is same as Starting Url,
                     * Exit the Loop, Otherwise Assign it to 'NextLinkUrl' variable
                     ***/
                    if (result.NextUrl.Equals(string.Empty) &&
                        result.NextUrl.Equals(NextChapterUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        LoopAgain = false;
                    }
                    else
                    {
                        NextLinkUrl = new Uri(result.NextUrl);
                    }
                }

            } while (LoopAgain);
        }

        /// <summary>
        /// To Download Only Single Chapter Page
        /// </summary>
        /// <param name="ChapterPageUrl">URL for Chapter Page</param>
        /// <param name="FilePath">Full File Path Containing FileName</param>
        /// <returns>Next Page Url</returns>
        public async Task<string> DownloadSingleChapterPage(Uri ChapterPageUrl, string FilePath)
        {
            //Download Html document
            string doc = await _download.LoadDocumentAsync(ChapterPageUrl).ConfigureAwait(false);
            //Parse the Html document to get List of Series
            var result = _parseHtml.GetChapterLinks(doc);
            //Download Image
            Uri ImageUrl = new Uri(result.ImageUrl);
            await _download.SaveImgAsync(ImageUrl, FilePath);

            return result.NextUrl;
        }
    }
}
