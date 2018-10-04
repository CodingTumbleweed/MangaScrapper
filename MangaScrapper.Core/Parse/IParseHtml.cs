using MangaScrapper.Core.Model.DataModel;
using MangaScrapper.Core.Model.Enum;
using System.Collections.Generic;

namespace MangaScrapper.Core.Parse
{
    interface IParseHtml
    {     
        /// <summary>
        /// Parses All Series Page/ All Chapter
        /// Page and returns Series/ Chapter List
        /// </summary>
        /// <param name="htmlDoc">Html page to be parsed</param>
        /// <param name="type">Type of List i.e. Serie or Chapter</param>
        /// <returns>List of Serie/Chapter objects</returns>
        IEnumerable<BaseModel> GetList(string htmlDoc, BaseType type);

        /// <summary>
        /// Parses Chapter Pages to retrieve
        /// Image Source and Next Page URL
        /// </summary>
        /// <param name="htmlDoc">Html page to be parsed</param>
        /// <returns>Image Source Url and Next Page Url</returns>
        ChapterModel GetChapterLinks(string htmlDoc);

    }
}
