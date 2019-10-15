using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SqlDbLite
{
    public class Check
    {
        public static void ThrowNotSupportedException(string message)
        {
            message = message.IsNullOrEmpty() ? new NotSupportedException().Message : message;
            throw new SqlDbLiteException("SqlDbLiteException.NotSupportedException：" + message);
        }

        public static void ArgumentNullException(object checkObj, string message)
        {
            if (checkObj == null)
                throw new SqlDbLiteException("SqlDbLiteException.ArgumentNullException：" + message);
        }

        public static void ArgumentNullException(object [] checkObj, string message)
        {
            if (checkObj == null|| checkObj.Length==0)
                throw new SqlDbLiteException("SqlDbLiteException.ArgumentNullException：" + message);
        }

        public static void Exception(bool isException, string message, params string[] args)
        {
            if (isException)
                throw new SqlDbLiteException(string.Format(message, args));
        }
    }
}
