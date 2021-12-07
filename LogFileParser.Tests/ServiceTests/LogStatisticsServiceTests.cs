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
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }

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
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }

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
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }

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
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }

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
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }

            // Assert
            Assert.AreEqual(3, service.GetTopVisitedUrls(3).Count());
            Assert.AreEqual("/intranet-analytics/", service.GetTopVisitedUrls(3).ElementAt(0));
            Assert.AreEqual("http://example.net/faq/", service.GetTopVisitedUrls(3).ElementAt(1));
            Assert.AreEqual("/this/page/does/not/exist/", service.GetTopVisitedUrls(3).ElementAt(2));
        }

        [TestMethod]
        public void GetTopVisitedUrls_ManyUrlsDifferentRanking_GetTop3()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDifferentLogLines.log");

            // Act
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }

            // Assert
            Assert.AreEqual(3, service.GetTopVisitedUrls(3).Count());
            Assert.AreEqual("/docs/manage-websites/", service.GetTopVisitedUrls(3).ElementAt(0));
            Assert.AreEqual("/this/page/does/not/exist/", service.GetTopVisitedUrls(3).ElementAt(1));
            Assert.AreEqual("/intranet-analytics/", service.GetTopVisitedUrls(3).ElementAt(2));
        }
    }
}
