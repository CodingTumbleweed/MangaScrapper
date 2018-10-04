using MangaScrapper.Core.Model.DataModel;
using System.Collections.Generic;

namespace MangaScrapper.Core.Common
{
    static class Mapper
    {
        public static XpathModel MapToXpathModel(ConfigurationModel config) 
        {
            XpathModel xpath = null;

            if (config != null)
            {
                xpath = new XpathModel();
                xpath.AllChapterXpath = config.AllChapterXpath;
                xpath.AllSeriesXpath = config.AllSeriesXpath;
                xpath.ImageXpath = config.ImageXpath;
                xpath.NextLinkXpath = config.NextLinkXpath;
            }
            return xpath;
        }
    }
}
