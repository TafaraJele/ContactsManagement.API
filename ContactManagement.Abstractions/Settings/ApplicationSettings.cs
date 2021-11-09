using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Abstractions.Settings
{
    public interface IApplicationSettings {

        /// <summary>
        /// 
        /// </summary>
        string VirtualDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string CommandServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string CommandDatabaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string QueryServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string QueryDatabaseName { get; set; }
        
    }

    public class ApplicationSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string VirtualDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommandServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommandDatabaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QueryServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QueryDatabaseName { get; set; }
        
    }
}
