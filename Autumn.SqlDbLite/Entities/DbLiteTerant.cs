using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class DbLiteTenant
    {
        public SqlDbLiteProvider Context { get; set; }
        public ConnectionConfig ConnectionConfig { get; set; }
    }
}
