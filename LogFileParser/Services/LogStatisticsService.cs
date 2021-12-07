﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
