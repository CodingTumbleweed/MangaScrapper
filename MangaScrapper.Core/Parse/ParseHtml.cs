using HtmlAgilityPack;
using MangaScrapper.Core.Common;
using MangaScrapper.Core.Model.DataModel;
using MangaScrapper.Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MangaScrapper.Core.Parse
{
    class ParseHtml : IParseHtml
    {
        private HtmlDocument _htmlDoc;
        private XpathModel _xpath;

        /// <summary>
        /// Takes XPath from Selected Site Configurations
        /// </summary>
        public ParseHtml() : this(null) { }

        public ParseHtml(XpathModel xpath)
        {
            _htmlDoc = new HtmlDocument();

            if (xpath == null)
            {
                if (DomainSettings.Configurations == null)
                    throw new MangaScrapperException("Configuration Missing");

                var SelectedConfiguration = DomainSettings.Configurations.GetSelected();
                _xpath = Mapper.MapToXpathModel(SelectedConfiguration);
            }
            else
            {
                _xpath = xpath;
            }

        }


        public IEnumerable<BaseModel> GetList(string htmlDoc, BaseType type)
        {
            if (string.IsNullOrEmpty(htmlDoc))
                throw new ArgumentNullException("htmlDoc (method GetList)");

            string ListXpath = null;
            List<BaseModel> ResultList = new List<BaseModel>();

            switch (type)
            {
                case BaseType.Serie:
                    ListXpath = _xpath.AllSeriesXpath;
                    break;
                case BaseType.Chapter:
                    ListXpath = _xpath.AllChapterXpath;
                    break;
            }

            _htmlDoc.LoadHtml(htmlDoc);
            var HtmlNodes = _htmlDoc.DocumentNode.SelectNodes(ListXpath);

            foreach (var node in HtmlNodes)
            {
                BaseModel item = new BaseModel();
                item.Name = node.InnerText;
                item.Url = node.Attributes["href"].Value;
                ResultList.Add(item);
            }
            return ResultList;
        }

        public ChapterModel GetChapterLinks(string htmlDoc)
        {
            if (string.IsNullOrEmpty(htmlDoc))
                throw new ArgumentNullException("htmlDoc (method Get)");

            ChapterModel Result = new ChapterModel();

            _htmlDoc.LoadHtml(htmlDoc);
            HtmlNodeCollection ImageNodes = _htmlDoc.DocumentNode.SelectNodes(_xpath.ImageXpath);
            HtmlNodeCollection NextLinkNodes = _htmlDoc.DocumentNode.SelectNodes(_xpath.NextLinkXpath);

            if (ImageNodes == null)
            {
                throw new MangaScrapperException("Image Node Not Found");
            }
            else
            {
                var ImageNode = ImageNodes.FirstOrDefault();
                Result.ImageUrl = ImageNode.Attributes["href"].Value;
            }

            if (NextLinkNodes == null)
            {
                //Not throwing exception as there might be cases where last
                //page of chapter doesn't point to next chapter
                Result.NextUrl = string.Empty;
            }
            else
            {
                var NextLinkNode = NextLinkNodes.FirstOrDefault();
                Result.NextUrl = NextLinkNode.Attributes["href"].Value;
            }

            return Result;
        }
    }
}
