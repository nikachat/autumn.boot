using Hangfire;
using Hangfire.Dashboard;
using Autumn.TaskScheduling.Filters;
using Autumn.TaskScheduling.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;

namespace Autumn.TaskScheduling
{
    public class Startup
    {

        /// <summary>
        /// Redis 服务
        /// </summary>
        public static ConnectionMultiplexer Redis;

        /// <summary>
        /// 配置接口
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                          .AddEnvironmentVariables();
            Configuration = builder.Build();
            Redis = ConnectionMultiplexer.Connect(Configuration.GetSection("RedisCaching").Value);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //返回大小写问题
            services.AddMvc()
                    .AddJsonOptions(option => option.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //注入服务
            services.AddHangfire(config => config.UseRedisStorage(Redis));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseHangfireServer();

            // 执行后台脚本，仅执行一次
            BackgroundJob.Enqueue(() => Jobs.Todo1());

            // 延迟执行后台脚本，仅执行一次 一天后执行
            BackgroundJob.Schedule(() => Jobs.Todo2(), TimeSpan.FromDays(1));

            // 每分钟运行一次
            RecurringJob.AddOrUpdate(() => Jobs.Todo3(), Cron.Minutely());

            // 周期性任务 每天凌晨2点运行任务，Cron参数使用的是UTC时间和北京时间有区别，需要转换下
            RecurringJob.AddOrUpdate(() => Jobs.Todo4(), Cron.Daily(18, 0));

            if (Convert.ToBoolean(Configuration.GetSection("HangFireAuth").Value))
            {
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new LoginDashboardAuthorizationFilter() }
                });
            }
            else
            {
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new AdministratorHangfireDashboardAuthorizationFilter() }
                });
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }

        /// <summary>
        /// 认证
        /// </summary>
        public class AdministratorHangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var user = context.GetHttpContext().User;
                //return user.Identity.IsAuthenticated && user.IsInRole("Administrator");
                return true;
            }
        }
    }
}
