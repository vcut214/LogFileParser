using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogFileParser.Helpers
{
    internal static class LogLineParser
    {
        private const string IpAddressRegex = "^(\\d+\\.){3}\\d+";
        private const string ResourceUrlSelectorRegex = "\"[A-Z]+ ([A-Za-z0-9:/.-]+) HTTP/\\d.\\d\"";

        /// <summary>
        /// Method that extracts the Host IP address from the log line.
        /// 
        /// If the IP address is of an invalid format, or it is missing, this will return blank.
        /// </summary>
        /// <param name="logLine"></param>
        /// <returns>String containing the host's IP address</returns>
        internal static string ExtractHostIpAddress(string logLine)
        {
            return Regex.Match(logLine, IpAddressRegex).Value;
        }

        /// <summary>
        /// Method that extracts the resource URL from the log line.
        /// 
        /// If the resource URL cannot be retrieved, this will return blank.
        /// </summary>
        /// <param name="logLine"></param>
        /// <returns>String containing the accessed resource URL</returns>
        internal static string ExtractResourceUrl(string logLine)
        {
            var matchGroups = Regex.Match(logLine, ResourceUrlSelectorRegex).Groups;

            if (matchGroups.TryGetValue("1", out Group resourceUrlMatchGroup))
                return resourceUrlMatchGroup.Value;

            return "";
        }
    }
}
