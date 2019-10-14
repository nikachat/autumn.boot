using System;
using AspNetCoreRateLimit;
using Autumn.Common;
using Autumn.FrameWork;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Autumn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            // 生成承载 web 应用程序的 Microsoft.AspNetCore.Hosting.IWebHost
            var host = CreateWebHostBuilder(args).Build();

            // 创建可用于解析作用域的服务
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                // get the IpPolicyStore instance
                var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();

                // seed IP data from appsettings
                 ipPolicyStore.SeedAsync();

                try
                {
                    // 从 system.IServicec提供程序获取 T 类型的服务
                    // 为了大家的数据安全，这里先注释掉了
                    // 数据库连接字符串是在Entity层的Seed 文件夹下的 DbContext.cs 中
                    var configuration = services.GetRequiredService<IConfiguration>();
                    if (configuration.GetSection("AppSettings")["SeedDBEnabled"].ToBool())
                    {
                        var dbContext = services.GetRequiredService<DbContext>();
                    }
                }
                catch (Exception e)
                {
                    NLogHelper.ErrorLog("数据库初始化失败。", e);
                    throw;
                }
            }

            // 运行 web 应用程序并阻止调用线程, 直到主机关闭
            // 创建完 WebHost 之后，便调用它的 Run 方法，而 Run 方法会去调用 WebHost 的 StartAsync 方法
            // 将Initialize方法创建的Application管道传入以供处理消息
            // 执行HostedServiceExecutor.StartAsync方法
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //使用预配置的默认值初始化
            WebHost.CreateDefaultBuilder(args)
                //指定要由 web 主机使用的启动类型
                //.UseUrls("http://localhost:6671")
                .UseStartup<Startup>();
    }
}
