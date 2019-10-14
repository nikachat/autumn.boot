using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Autumn.Common
{
    /// <summary>
    /// 单号生成类
    /// </summary>
    public class SingleNumberHelper
    {
        /// <summary>
        /// 平台主流水号HydNo
        /// </summary>
        /// <returns></returns>
        public static string GetPlatformMasterNumber()
        {
            RedisHelper redis = new RedisHelper(DbNum.GlobalUniqueNumber.ToInt());
            var dtime = DateTime.Now;
            //后两位年
            var yy = dtime.ToString("yy");
            //当前周是当前年的第几周
            var weekOfYear = WeekOfYear(dtime);
            if (weekOfYear.Length != 2)
            {
                weekOfYear = "0" + weekOfYear;
            }
            //当前周的第几天
            int day = (int)dtime.DayOfWeek;
            if (day == 0)
                day = 7;
            //两位随机码
            var num2 = RandomNumber(2);
            //四位序列号
            var serial_num = RedisItemNumber(redis, "HydNo");
            //三位随机码
            var num3 = RandomNumber(3);
            return yy + weekOfYear + day.ToString() + num2 + serial_num + num3;
        }

        /// <summary>
        /// 平台子流水号
        /// </summary>
        /// <returns></returns>
        public static string GetPlatformSubflowNumber(string number)
        {
            RedisHelper redis = new RedisHelper(DbNum.GlobalUniqueNumber.ToInt());
            //两位序列号
            var serial_num_two = RedisSerialNum(redis, number);
            return number + serial_num_two;
        }


        #region 编号生成所需共通方法

        /// <summary>
        /// 获取当天的编号
        /// </summary>
        /// <param name="redis">缓存</param>
        /// <param name="length">编号长度</param>
        /// <param name="keyDate">编号日期</param>
        /// <param name="keyNumber">缓存计数</param>
        /// <returns></returns>
        public static string RedisItemNumber(RedisHelper redis, string keyNumber, int length = 4, string keyDate = "ItemDayOfYear")
        {
            // 编号
            double num = 0;
            if (!redis.KeyExists(keyDate)) redis.StringSet(keyDate, DateTime.Now.DayOfYear);

            if (DateTime.Now.DayOfYear.ToString() == redis.StringGet(keyDate))
                num = redis.StringIncrement(keyNumber);
            else
            {
                redis.StringSet(keyDate, DateTime.Now.DayOfYear);
                redis.KeyDelete(keyNumber);
                num = redis.StringIncrement(keyNumber);
            }
            return num.ToString().PadLeft(length, '0');
        }

        /// <summary>
        /// 随机码获取
        /// </summary>
        /// <param name="redis">RedisHelper 定义</param>
        /// <param name="number">长度</param>
        /// <returns></returns>
        public static string RedisSerialNum(RedisHelper redis, string number)
        {
            // 编号
            double num = 0;
            if (!redis.KeyExists(number)) redis.StringSet(number, new TimeSpan(1200000000));

            if (DateTime.Now.DayOfYear.ToString() == redis.StringGet(number))
                num = redis.StringIncrement(number + "num");
            else
            {
                redis.StringSet(number, new TimeSpan(1200000000));
                redis.KeyDelete(number + "num");
                num = redis.StringIncrement(number + "num");
            }
            return num.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 当前周是当前年的第几周
        /// </summary>
        /// <param name="dtime">当前日期</param>
        /// <returns></returns>
        public static string WeekOfYear(DateTime dtime)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dtime, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="length">随机数的长度</param>
        /// <returns></returns>
        public static string RandomNumber(int length)
        {
            if (length == 0)
                return "";
            var str = "";
            var num = "";
            for (int i = 0; i < length; i++)
            {
                num += "9";
            }
            Random rad = new Random();//实例化随机数产生器rad；
            int value = rad.Next(0, Int32.Parse(num));//用rad生成大于等于1000，小于等于9999的随机数；
            //补充长度
            for (int i = 0; i < length - value.ToString().Length; i++)
            {
                str = "0" + str;
            }
            str = str + value.ToString();//长度补充拼接
            return str;
        }

        #endregion
    }
}
