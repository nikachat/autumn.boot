using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;

namespace SqlDbLite
{
    internal class CallContext
    {
        public static ThreadLocal<List<SqlDbLiteProvider>> ContextList = new ThreadLocal<List<SqlDbLiteProvider>>();
    }
}
