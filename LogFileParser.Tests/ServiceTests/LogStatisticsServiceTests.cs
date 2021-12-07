using LogFileParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
            var logString = "168.41.191.40 - - [09/Jul/2018:10:10:38 +0200] \"GET http://example.net/blog/category/meta/ HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_7) AppleWebKit/534.24 (KHTML, like Gecko) RockMelt/0.9.58.494 Chrome/11.0.696.71 Safari/534.24\"";

            // Act
            service.ParseLogLine(logString);

            // Assert
            Assert.AreEqual(1, service.GetNumberOfDistinctIpAddresses());
        }

        [TestMethod]
        public void ParseLogLine_ManySameIpAddresses()
        {
            // Arrange
            var service = new LogStatisticsService();
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManySameIpAddresses.log");

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
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), TestDataSubdirectory, "ManyDifferentIpAddresses.log");

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
    }
}
