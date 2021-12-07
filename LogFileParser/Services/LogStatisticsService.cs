using LogFileParser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogFileParser.Services
{
    public class LogStatisticsService
    {
        /*
         * The task is to parse a log file containing HTTP requests and to report on its contents. For a given log file we
         *   want to know:
         *  ● The number of unique IP addresses
         *  ● The top 3 most visited URLs
         *  ● The top 3 most active IP addresses
         * 
         */
        private Dictionary<string, int> _ipAddressCounts;
        private Dictionary<string, int> _urlCounts;

        public LogStatisticsService()
        {
            _ipAddressCounts = new Dictionary<string, int>();
            _urlCounts = new Dictionary<string, int>();
        }

        public void ParseLogLine(string logLineString)
        {
            string ipAddress = LogLineParser.ExtractHostIpAddress(logLineString);
            if (!_ipAddressCounts.ContainsKey(ipAddress))
                _ipAddressCounts.Add(ipAddress, 0);

            _ipAddressCounts[ipAddress]++;

            string resourceUrl = LogLineParser.ExtractResourceUrl(logLineString);
            if (!_urlCounts.ContainsKey(resourceUrl))
                _urlCounts.Add(resourceUrl, 0);

            _urlCounts[resourceUrl]++;
        }

        /// <summary>
        /// Gets the number of distinct Host IP addresses in the log file so far.
        /// </summary>
        /// <returns>Number of distinct IP addresses found in the log file</returns>
        public int GetNumberOfDistinctIpAddresses()
        {
            return _ipAddressCounts.Keys.Count;
        }

        /// <summary>
        /// Gets the top X visited URLs in the log file.
        /// 
        /// For URLs that have the same ranking, the earliest accessed URL will be used.
        /// </summary>
        /// <param name="numberOfUrlsToSelect"></param>
        /// <returns>List of URLs ranked by number of instances in the log file</returns>
        public IEnumerable<string> GetTopVisitedUrls(int numberOfUrlsToSelect)
        {
            var orderedUrlCounts = _urlCounts.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return orderedUrlCounts.Select(x => x.Key).Take(numberOfUrlsToSelect);
        }

        /// <summary>
        /// Gets the top X most active IP addresses in the log file.
        /// 
        /// For IP addresses that have the same ranking, the earliest IP addresses will be used.
        /// </summary>
        /// <param name="numberOfUrlsToSelect"></param>
        /// <returns>List of IP addresses ranked by number of instances in the log file</returns>
        public IEnumerable<string> GetTopActiveIpAddresses(int numberOfIpAddressesToSelect)
        {
            var orderedIpAddressCounts = _ipAddressCounts.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return orderedIpAddressCounts.Select(x => x.Key).Take(numberOfIpAddressesToSelect);
        }
    }
}
