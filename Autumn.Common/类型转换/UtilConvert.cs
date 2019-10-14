using System;

namespace Autumn.Common
{
    ///<summary>
    ///对象类型转换类
    ///</summary>
    public static class UtilConvert
    {
        ///<summary>
        ///对象转整型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<returns></returns>
        public static int ToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        ///<summary>
        ///对象转整型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<param name="errorValue"></param>
        ///<returns></returns>
        public static int ToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        ///<summary>
        ///对象转浮点型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<returns></returns>
        public static double ToMoney(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        ///<summary>
        ///对象转浮点型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<param name="errorValue"></param>
        ///<returns></returns>
        public static double ToMoney(this object thisValue, double errorValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        ///<summary>
        ///对象转字符型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<returns></returns>
        public static string ToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
        ///<summary>
        ///对象转字符型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<param name="errorValue"></param>
        ///<returns></returns>
        public static string ToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }
        ///<summary>
        ///对象转浮点型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<returns></returns>
        public static Decimal ToDecimal(this object thisValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        ///<summary>
        ///对象转浮点型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<param name="errorValue"></param>
        ///<returns></returns>
        public static Decimal ToDecimal(this object thisValue, decimal errorValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        ///<summary>
        ///对象转日期型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<returns></returns>
        public static DateTime ToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }
        ///<summary>
        ///对象转日期型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<param name="errorValue"></param>
        ///<returns></returns>
        public static DateTime ToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        ///<summary>
        ///对象转布尔型
        ///</summary>
        ///<param name="thisValue"></param>
        ///<returns></returns>
        public static bool ToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
    }
}
