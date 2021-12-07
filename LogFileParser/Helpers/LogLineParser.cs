using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogFileParser.Helpers
{
    public static class LogLineParser
    {
        private const string IpAddressRegex = "^(\\d+\\.){3}\\d+";

        /// <summary>
        /// Method that extracts the Host IP address from the log line.
        /// 
        /// If the IP address is of an invalid format, or it is missing, this will return blank.
        /// </summary>
        /// <param name="logLine"></param>
        /// <returns>String containing the host's IP address</returns>
        public static string ExtractHostIpAddress(string logLine)
        {
            return Regex.Match(logLine, IpAddressRegex).Value;
        }
    }
}
