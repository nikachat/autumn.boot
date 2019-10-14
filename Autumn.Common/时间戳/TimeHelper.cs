 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autumn.Common.时间戳
{
   public class TimeHelper
    {
        /// <summary>
		/// 获取以0点0分0秒开始的日期
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static DateTime GetStartDateTime(DateTime d)
		{
			if (d.Hour != 0)
			{
				var year = d.Year;
				var month = d.Month;
				var day = d.Day;
				var hour = "0";
				var minute = "0";
				var second = "0";
				d = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, second));
			}
			return d;
		}

		/// <summary>
		/// 获取以23点59分59秒结束的日期
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static DateTime GetEndDateTime(DateTime d)
		{
			if (d.Hour != 23)
			{
				var year = d.Year;
				var month = d.Month;
				var day = d.Day;
				var hour = "23";
				var minute = "59";
				var second = "59";
				d = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, second));
			}
			return d;
		}
    }
}
