using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class QueueItem
    {
        public string Sql { get; set; }
        public DbLiteParameter[] Parameters { get; set; }
    }
}
