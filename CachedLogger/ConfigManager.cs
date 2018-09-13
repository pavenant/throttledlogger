using System;
using System.Collections.Generic;
using System.Text;

namespace CachedLoggerHost
{

    public class ConfigManager : IConfigManager
    {
        public int TimerInterval { get; set; }
        public bool UseLog4Net
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if config changed</returns>
        public bool Refresh()
        {
            //read latest config from config file...
            Console.WriteLine("read latest config from config file");
            return false;
        }

    }
}
