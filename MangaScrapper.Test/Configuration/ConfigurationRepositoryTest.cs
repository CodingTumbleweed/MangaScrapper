using NUnit.Framework;
using System.Linq;
using MangaScrapper.Core.Configuration;
using MangaScrapper.Core.Model.DataModel;

namespace MangaScrapper.Test.Configuration
{
    [TestFixture]
    class ConfigurationRepositoryTest
    {
        private ConfigurationRepository _sourceConfig;
        private ConfigurationModel source = new ConfigurationModel
            {
                Name = "TestSource",
                AllSeriesUrl = "http://www.testscrapper.com/allseries",
                AllSeriesXpath = "//ul/li/a",
                AllChapterXpath = "//ul/li/a",
                ImageXpath = "//a[1]",
                NextLinkXpath = "//body/a[last()]"
            };

        [SetUp]
        public void SetUp()
        {
            _sourceConfig = new ConfigurationRepository();
            _sourceConfig.FileName = "TestConfig.dat";
        }

        [TearDown]
        public void TearDown()
        {
            _sourceConfig = null;
        }

        [Test]
        [Category("QuickTests")]
        public void ShouldSaveConfiguration()
        {
            var lstSource = new Configurations();
            //Adding to source list
            lstSource.AddConfiguration(source);
            //Saves source list
            _sourceConfig.SaveSourceConfig(lstSource);
        }

        [Test]
        public void ShouldReadConfiguration()
        {
            //Check if File Exists. If Not Write to file
            if (!_sourceConfig.FileExists())
            {
                var lstSource = new Configurations();
                //Adding to source list
                lstSource.AddConfiguration(source);
                //Saves source list
                _sourceConfig.SaveSourceConfig(lstSource);
            }

            //Reads source list
            var readSource = _sourceConfig.ReadSourceConfig();
            //Asserts that 'source' object is equal to read list item
            Assert.AreEqual(readSource.ConfigurationList.FirstOrDefault(), source);
        }

    }
}
