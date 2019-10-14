using Polly;
using System;
using System.Collections;

namespace Autumn.Common
{
    /// <summary>
    /// 重试机制帮助类
    /// </summary>
    public class RetryHelper
    {
        public delegate void DelegateEvent();
        public delegate void DelegateEventParameter(object parameter);

        /// <summary>
        /// 直接重试
        /// </summary>
        /// <param name="MethodName"></param>
        /// <param name="RetryTimes"></param>
        public static void Direct(DelegateEvent MethodName, int RetryTimes = 3)
        {
            try
            {
                var retryTwoTimesPolicy = Policy
               .Handle<Exception>()
               .Retry(RetryTimes, (ex, count) =>
               {
                   NLogHelper.ErrorLog(MethodName.Method.Name.ToString() + "重试次数：" + count, ex);
               });
                retryTwoTimesPolicy.Execute(() =>
                {
                    MethodName();
                });
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorLog("重试异常：", ex);
            }
        }

        /// <summary>
        /// 直接重试
        /// </summary>
        /// <param name="MethodName"></param>
        /// <param name="ParameterList"></param>
        /// <param name="RetryTimes"></param>
        public static void Direct(DelegateEventParameter MethodName, ArrayList ParameterList, int RetryTimes = 3)
        {
            try
            {
                var retryTwoTimesPolicy = Policy
               .Handle<Exception>()
               .Retry(RetryTimes, (ex, count) =>
               {
                   NLogHelper.ErrorLog(MethodName.Method.Name.ToString() + "重试次数：" + count, ex);
               });
                retryTwoTimesPolicy.Execute(() =>
                {
                    if (ParameterList != null && ParameterList.Count == 1)
                        MethodName(ParameterList[0].ToString());
                    else
                        NLogHelper.ErrorLog(MethodName.Method.Name.ToString() + "参数异常。");
                });
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorLog("重试异常：", ex);
            }
        }

        /// <summary>
        /// 间隔重试（无参数）间隔时间5秒、15秒、60秒
        /// </summary>
        /// <param name="MethodName"></param>
        public static void Interval(DelegateEvent MethodName)
        {
            try
            {
                var politicaWaitAndRetry = Policy
                 .Handle<Exception>()
                 .WaitAndRetry(new[]
                 {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(15),
                    TimeSpan.FromSeconds(60)
                 }, ReportaError);
                politicaWaitAndRetry.Execute(() =>
                {
                    MethodName();
                });
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorLog("重试异常：", ex);
            }
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="e"></param>
        /// <param name="tiempo"></param>
        /// <param name="intento"></param>
        /// <param name="contexto"></param>
        private static void ReportaError(Exception e, TimeSpan tiempo, int intento, Context contexto)
        {
            NLogHelper.ErrorLog("重试异常：", e);
            Console.WriteLine($"异常: {intento:00} (调用秒数: {tiempo.Seconds} 秒)\t执行时间: {DateTime.Now}");
        }

    }
}
