using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class SqlFuncExternal
    {
        public string UniqueMethodName { get; set; }
        public Func<MethodCallExpressionModel, DbType, ExpressionContext, string> MethodValue { get; set; }
    }
}
