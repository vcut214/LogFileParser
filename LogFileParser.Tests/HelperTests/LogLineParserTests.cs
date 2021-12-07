using LogFileParser.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileParser.Tests.HelperTests
{
    [TestClass]
    public class LogLineParserTests
    {
        [TestMethod]
        public void ExtractHostIpAddress_ValidIpAddress()
        {
            // Arrange
            var logString = "168.41.191.40 - - [09/Jul/2018:10:10:38 +0200] \"GET http://example.net/blog/category/meta/ HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            var result = LogLineParser.ExtractHostIpAddress(logString);

            // Assert
            Assert.AreEqual("168.41.191.40", result);
        }

        [TestMethod]
        public void ExtractHostIpAddress_InvalidIpAddress()
        {
            // Arrange
            var logString = "xxx.xx.xxx.xx - - [09/Jul/2018:10:10:38 +0200] \"GET http://example.net/blog/category/meta/ HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            var result = LogLineParser.ExtractHostIpAddress(logString);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ExtractHostIpAddress_MissingIpAddress()
        {
            // Arrange
            var logString = " - - [09/Jul/2018:10:10:38 +0200] \"GET http://example.net/blog/category/meta/ HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            var result = LogLineParser.ExtractHostIpAddress(logString);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ExtractResourceUrl_ValidResourceUrl()
        {
            // Arrange
            var logString = "168.41.191.40 - - [09/Jul/2018:10:10:38 +0200] \"GET http://example.net/blog/category/meta/ HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            var result = LogLineParser.ExtractResourceUrl(logString);

            // Assert
            Assert.AreEqual("http://example.net/blog/category/meta/", result);
        }

        [TestMethod]
        public void ExtractResourceUrl_InvalidResourceUrl()
        {
            // Arrange
            var logString = "168.41.191.40 - - [09/Jul/2018:10:10:38 +0200] \"GET &%* HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            var result = LogLineParser.ExtractResourceUrl(logString);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ExtractResourceUrl_MissingResourceUrl()
        {
            // Arrange
            var logString = "168.41.191.40 - - [09/Jul/2018:10:10:38 +0200] \"GET HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            var result = LogLineParser.ExtractResourceUrl(logString);

            // Assert
            Assert.AreEqual("", result);
        }
    }
}
