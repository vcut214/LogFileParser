using LogFileParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace LogFileParser.Tests.ServiceTests
{
    [TestClass]
    public class LogStatisticsServiceTests
    {
        private const string TestDataSubdirectory = "ServiceTests/Test Data";
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange / Act
            var service = new LogStatisticsService();

            // Assert
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ParseLogLine_OneIpAddress()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "SingleLogLine.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(1, service.GetNumberOfDistinctIpAddresses());
        }

        [TestMethod]
        public void ParseLogLine_ManySameIpAddresses()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManySameLogLines.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(1, service.GetNumberOfDistinctIpAddresses());
        }

        [TestMethod]
        public void ParseLogLine_ManyDifferentIpAddresses()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDifferentLogLines.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(3, service.GetNumberOfDistinctIpAddresses());
        }

        [TestMethod]
        public void GetTopVisitedUrls_OneUrl_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "SingleLogLine.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(1, service.GetTopVisitedUrls(3).Count());
            Assert.AreEqual("http://example.net/blog/category/meta/", service.GetTopVisitedUrls(3).FirstOrDefault());
        }

        [TestMethod]
        public void GetTopVisitedUrls_ManyUrlsSameRanking_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDistinctLogLines.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(3, service.GetTopVisitedUrls(3).Count());
            Assert.AreEqual("/docs/manage-websites/", service.GetTopVisitedUrls(3).ElementAt(0));
            Assert.AreEqual("/blog/2018/08/survey-your-opinion-matters/", service.GetTopVisitedUrls(3).ElementAt(1));
            Assert.AreEqual("http://example.net/blog/category/meta/", service.GetTopVisitedUrls(3).ElementAt(2));
        }

        [TestMethod]
        public void GetTopVisitedUrls_ManyUrlsDifferentRanking_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDifferentLogLines.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(3, service.GetTopVisitedUrls(3).Count());
            Assert.AreEqual("/docs/manage-websites/", service.GetTopVisitedUrls(3).ElementAt(0));
            Assert.AreEqual("/this/page/does/not/exist/", service.GetTopVisitedUrls(3).ElementAt(1));
            Assert.AreEqual("/blog/2018/08/survey-your-opinion-matters/", service.GetTopVisitedUrls(3).ElementAt(2));
        }
        [TestMethod]
        public void GetTopActiveIpAddresses_OneIpAddress_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "SingleLogLine.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(1, service.GetTopActiveIpAddresses(3).Count());
            Assert.AreEqual("168.41.191.40", service.GetTopActiveIpAddresses(3).FirstOrDefault());
        }

        [TestMethod]
        public void GetTopActiveIpAddresses_ManyIpAddressesSameRanking_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDistinctLogLines.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(3, service.GetTopActiveIpAddresses(3).Count());
            Assert.AreEqual("168.41.191.44", service.GetTopActiveIpAddresses(3).ElementAt(0));
            Assert.AreEqual("177.71.128.22", service.GetTopActiveIpAddresses(3).ElementAt(1));
            Assert.AreEqual("168.41.191.43", service.GetTopActiveIpAddresses(3).ElementAt(2));
        }

        [TestMethod]
        public void GetTopActiveIpAddresses_ManyIpAddressesDifferentRanking_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDifferentLogLines.log");

            // Act
            service.ParseLog(logFilePath);

            // Assert
            Assert.AreEqual(3, service.GetTopActiveIpAddresses(3).Count());
            Assert.AreEqual("168.41.191.40", service.GetTopActiveIpAddresses(3).ElementAt(0));
            Assert.AreEqual("177.71.128.21", service.GetTopActiveIpAddresses(3).ElementAt(1));
            Assert.AreEqual("168.41.191.41", service.GetTopActiveIpAddresses(3).ElementAt(2));
        }

    }
}
