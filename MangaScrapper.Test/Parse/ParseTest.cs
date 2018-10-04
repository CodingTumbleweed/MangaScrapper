using MangaScrapper.Core.Model.DataModel;
using MangaScrapper.Core.Model.Enum;
using MangaScrapper.Core.Parse;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace MangaScrapper.Test.Parse
{
    [TestFixture]
    class ParseTest
    {
        private IParseHtml _objParse;
        private XpathModel _xPath;
        private StringBuilder _html;

        [SetUp]
        public void SetUp()
        {
            _xPath = new XpathModel()
            {
                AllSeriesXpath = "//ul/li/a",
                AllChapterXpath = "//ul/li/a",
                ImageXpath = "//a[1]",
                NextLinkXpath = "//body/a[last()]"
            };
            _objParse = new ParseHtml(_xPath);

            _html = new StringBuilder();
            _html.Append("<HTML>");
            _html.Append("<HEAD>");
            _html.Append("<TITLE>Your Title Here</TITLE>");
            _html.Append("</HEAD>");
            _html.Append("<BODY BGCOLOR=\"FFFFFF\">");
            _html.Append("<a href=\"http://testsite.com\">Link Name</a>");
            _html.Append("<H1>This is a Header</H1>");
            _html.Append("<H2>This is a Medium Header</H2>");
            _html.Append("<ul>");
            _html.Append("<li><a href=\"item1.com\">This is List item 1</a></li>");
            _html.Append("<li><a href=\"item2.com\">This is List item 2</a></li>");
            _html.Append("<li><a href=\"item3.com\">This is List item 3</a></li>");
            _html.Append("<li><a href=\"item4.com\">This is List item 4</a></li>");
            _html.Append("</ul>");
            _html.Append("Send me mail at <a href=\"mailto:support@yourcompany.com\">support@yourcompany.com</a>.");
            _html.Append("<P> This is a new paragraph!");
            _html.Append("<P> <B>This is a new paragraph!</B>)");
            _html.Append("<BR> <B><I>This is a new sentence without a paragraph break, in bold italics.</I></B>");
            _html.Append("<HR>");
            _html.Append("</BODY>");
            _html.Append("</HTML>");
        }

        [TearDown]
        public void TearDown()
        {
            _objParse = null;
            _html = null;
        }

        [Test]
        public void ParseChapter()
        {
            ChapterModel _parseChapterResult = new ChapterModel();
            _parseChapterResult.ImageUrl = "http://testsite.com";
            _parseChapterResult.NextUrl = "mailto:support@yourcompany.com";

            var result = _objParse.GetChapterLinks(_html.ToString());
            Assert.AreEqual(_parseChapterResult, result);
        }

        [Test]
        public void ParseList()
        {

            List<BaseModel> _parseListResult = new List<BaseModel>();
            _parseListResult.Add(new BaseModel() { Name = "This is List item 1", Url = "item1.com" });
            _parseListResult.Add(new BaseModel() { Name = "This is List item 2", Url = "item2.com" });
            _parseListResult.Add(new BaseModel() { Name = "This is List item 3", Url = "item3.com" });
            _parseListResult.Add(new BaseModel() { Name = "This is List item 4", Url = "item4.com" });

            var result = _objParse.GetList(_html.ToString(), BaseType.Serie);
            Assert.AreEqual(_parseListResult, result);
        }
    }
}
