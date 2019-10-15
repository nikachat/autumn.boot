using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public partial interface IDMLBuilder
    {
        string SqlTemplate { get; }
        List<DbLiteParameter> Parameters { get; set; }
        SqlDbLiteProvider  Context { get; set; }
        StringBuilder sql { get; set; }
        string ToSqlString();
        void Clear();
    }
}
