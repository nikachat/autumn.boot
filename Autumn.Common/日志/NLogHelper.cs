using NLog;
using System;

namespace Autumn.Common
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public class NLogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public static void ErrorLog(string throwMsg, Exception ex = null)
        {
            string errorMsg = string.Empty;

            if(ex==null)
                errorMsg = string.Format("\r\n【描述】：{0}", new object[] { throwMsg });
            else
                errorMsg = string.Format("\r\n【描述】：{0}\r\n【堆栈】：{1}", new object[] { throwMsg, ex.StackTrace.TrimStart() });

            logger.Error(errorMsg);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="operateMsg"></param>
        public static void DebugLog(string operateMsg)
        {
            string Msg = string.Format("\r\n【描述】：{0}", new object[] { operateMsg });

            logger.Debug(Msg);
        }
    }
}
