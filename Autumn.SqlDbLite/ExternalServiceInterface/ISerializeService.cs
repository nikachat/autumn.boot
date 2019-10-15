using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    public interface ISerializeService
    {
        string SerializeObject(object value);
        string DbLiteSerializeObject(object value);
        T DeserializeObject<T>(string value);
    }
}
