using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

namespace Autumn.Common
{
    ///<summary>
    ///配置文件操作类
    ///</summary>
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }

        static Appsettings()
        {
            string Path = "appsettings.json";

            Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })
               .Build();
        }

        ///<summary>
        ///要操作的字符
        ///</summary>
        ///<param name="sections"></param>
        ///<returns></returns>
        public static string Get(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }

                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
