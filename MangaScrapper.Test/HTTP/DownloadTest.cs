using MangaScrapper.Core.HTTP;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;


namespace MangaScrapper.Test.HTTP
{
    [TestFixture]
    class DownloadTest
    {
        private IDownload _content;
        private Uri uri, imageUri;
        private string imageName, imageNameNoExtension,
            imagePath, fileExtension;

        [SetUp]
        public void SetUp()
        {
            uri = new Uri("https://www.google.com/");
            imageUri = new Uri("https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");
            imageName = "TestImage.jpg";
            imageNameNoExtension = "TestImageNoExtension";
            fileExtension = imageUri.GetFileExtension();
            _content = new DownloadAsync();
        }

        [TearDown]
        public void TearDown()
        {
            _content = null;
            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }

        [Test]
        public async Task ShouldLoadDocument()
        {
            string doc = await _content.LoadDocumentAsync(uri);
            Assert.IsNotEmpty(doc);
        }

        [Test]
        public async Task ShouldSaveImage()
        {
            imagePath = Path.Combine(Path.GetTempPath(), imageName);
            await _content.SaveImgAsync(imageUri, imagePath);
            FileAssert.Exists(imagePath);
        }

        [Test]
        public async Task ShouldSaveImageWithNoExtension()
        {
            imagePath = Path.Combine(Path.GetTempPath(), imageNameNoExtension);
            await _content.SaveImgAsync(imageUri, imagePath);
            //Adding Extension for File Check
            imagePath += fileExtension;
            FileAssert.Exists(imagePath);
        }

        [Test]
        public async Task ShouldThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _content.SaveImgAsync(null, null));

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _content.SaveImgAsync(null, imagePath));

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _content.SaveImgAsync(uri, null));

            Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _content.LoadDocumentAsync(null));
        }
    }
}
