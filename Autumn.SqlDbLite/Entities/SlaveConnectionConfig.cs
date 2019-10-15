using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class SlaveConnectionConfig
    {
        /// <summary>
        ///Default value is 1
        ///If value is 0 means permanent non execution
        /// </summary>
        public int HitRate = 1;
        public string ConnectionString { get; set; }
    }
}
