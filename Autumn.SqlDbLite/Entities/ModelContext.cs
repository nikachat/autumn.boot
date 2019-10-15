using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public class ModelContext
    {
        [DbLiteColumn(IsIgnore = true)]
        [JsonIgnore]
        public SqlDbLiteProvider Context { get; set; }
        public IDbLiteQueryable<T> CreateMapping<T>() where T : class, new()
        {
            Check.ArgumentNullException(Context, "Please use Sqlugar.ModelContext");
            using (Context)
            {
                return Context.Queryable<T>();
            }
        }
    }
}
