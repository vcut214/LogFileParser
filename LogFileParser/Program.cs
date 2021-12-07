using LogFileParser.Services;
using System;
using System.IO;

namespace LogFileParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No file path argument detected!");
                Console.WriteLine();
                Console.WriteLine("Please use the application in this form");
                Console.WriteLine("No file path argument detected!");

            }
            var service = new LogStatisticsService();
            var logFilePath = args[0];

            // Act
            // Read the file and display it line by line.  
            foreach (string logLine in File.ReadLines(logFilePath))
            {
                if (!string.IsNullOrWhiteSpace(logLine))
                    service.ParseLogLine(logLine);
            }
        }
    }
}
