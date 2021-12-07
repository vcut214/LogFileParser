using LogFileParser.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogFileParser.Tests.ServiceTests
{
    [TestClass]
    public class LogStatisticsServiceTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange / Act
            var service = new LogStatisticsService();

            // Assert
            Assert.IsNotNull(service);
        }
    }
}
