using System;
using NUnit.Framework;
using MangaScrapper.Core.Logging;


namespace MangaScrapper.Test.Logging
{
    [TestFixture]
    class LogTest
    {
        [Test]
        public void ShouldWriteInfoLog() 
        {
            Log.Info("Writing Info Test Message");
        }

        [Test]
        public void ShouldWriteInfoLogWithName()
        {
            Log.Info("Writing Info Test Message", "Test");
        }

        [Test]
        public void ShouldWriteErrorLog()
        {
            Log.Error("Writing Error Test Message");
        }

        [Test]
        public void ShouldWriteErrorLogWithName()
        {
            Log.Error("Writing Error Test Message", "Test");
        }

        [Test]
        public void ShouldWriteErrorLogWithException()
        {
            Log.Error("Writing Error Test Message", new ArgumentNullException("Test Argument Missing"));
        }

        [Test]
        public void ShouldWriteFatalLog()
        {
            Log.Error("Writing Fatal Test Message");
        }

        [Test]
        public void ShouldWriteFatalLogWithName()
        {
            Log.Error("Writing Fatal Test Message", "Test");
        }

        [Test]
        public void ShouldWriteFatalLogWithException()
        {
            Log.Error("Writing Fatal Test Message", new ArgumentNullException("Test Argument Missing"));
        }
    }
}
