using System.IO;
using System.Net;
using System.Text;

namespace Autumn.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="serviceAddress">地址</param>
        /// <returns></returns>
        public static string Get(string serviceAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="serviceAddress">地址</param>
        /// <param name="strContent">键值对"{ ""name"": ""value""}"</param>
        /// <returns></returns>
        public static string Post(string serviceAddress,object strContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();

            return retString;
        }

        /// <summary>
        /// POST请求支持重定向
        /// </summary>
        /// <param name="posturl">地址</param>
        /// <param name="postData">参数</param>
        /// <param name="contentType">类型[application/json]、[application/x-www-form-urlencoded]</param>
        /// <returns></returns>
        public static string Post(string posturl, string postData,string contentType= "application/x-www-form-urlencoded")
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postData);
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();

                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = false;
                request.ProtocolVersion = HttpVersion.Version11;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                //request.Host = "";
                //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0";
                //request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                request.ContentType = contentType;
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return "";
            }
            catch (WebException ex)
            {
                NLogHelper.ErrorLog(ex.Message);
                // 302重定向
                return ex.Response.Headers["Location"].ToString();
            }
        }
    }
}
