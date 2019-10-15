using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class AopProvider
    {
        private AopProvider() { }
        public AopProvider(SqlDbLiteProvider context)
        {
            this.Context = context;
            this.Context.Ado.IsEnableLogEvent = true;
        }
        private SqlDbLiteProvider Context { get; set; }
        public Action<DiffLogModel> OnDiffLogEvent { set { this.Context.CurrentConnectionConfig.AopEvents.OnDiffLogEvent = value; } }
        public Action<SqlDbLiteException> OnError { set { this.Context.CurrentConnectionConfig.AopEvents.OnError = value; } }
        public Action<string, DbLiteParameter[]> OnLogExecuting { set { this.Context.CurrentConnectionConfig.AopEvents.OnLogExecuting= value; } }
        public Action<string, DbLiteParameter[]> OnLogExecuted { set { this.Context.CurrentConnectionConfig.AopEvents.OnLogExecuted = value; } }
        public Func<string, DbLiteParameter[], KeyValuePair<string, DbLiteParameter[]>> OnExecutingChangeSql { set { this.Context.CurrentConnectionConfig.AopEvents.OnExecutingChangeSql = value; } }
    }
}
