using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
namespace SqlDbLite
{
    public class SqlDbLiteException : Exception
    {
        public  string Sql { get; set; }
        public  object Parametres { get; set; }
        public new Exception InnerException;
        public new string  StackTrace;
        public new MethodBase TargetSite;
        public new string Source;

        public SqlDbLiteException(string message)
            : base(message){}

        public SqlDbLiteException(SqlDbLiteProvider context,string message, string sql)
            : base(message) {
            this.Sql = sql;
        }

        public SqlDbLiteException(SqlDbLiteProvider context, string message, string sql, object pars)
            : base(message) {
            this.Sql = sql;
            this.Parametres = pars;
        }

        public SqlDbLiteException(SqlDbLiteProvider context, Exception ex, string sql, object pars)
            : base(ex.Message)
        {
            this.Sql = sql;
            this.Parametres = pars;
            this.InnerException = ex.InnerException;
            this.StackTrace = ex.StackTrace;
            this.TargetSite = ex.TargetSite;
            this.Source = ex.Source;
        }

        public SqlDbLiteException(SqlDbLiteProvider context, string message, object pars)
            : base(message) {
            this.Parametres = pars;
        }
    }
    public class VersionExceptions : SqlDbLiteException
    {
        public VersionExceptions(string message)
            : base(message){ }
    }
}
