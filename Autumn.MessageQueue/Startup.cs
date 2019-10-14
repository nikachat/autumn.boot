using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autumn.Common;
using Autumn.MessageQueue.Extension;
using Autumn.MessageQueue.IServices;
using Autumn.MessageQueue.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Autumn.MessageQueue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Kafka
            // 接收
            services.AddTransient<ISubscriberService, SubscriberService>();

            services.AddCap(x =>
            {
                //if (BaseDBConfig.DbType == DataBaseType.SqlServer)
                //    x.UseSqlServer(BaseDBConfig.ConnectionString);
                //else
                    x.UseMySql(DBConfigHelper.ConnectionString);

                x.UseKafka(Def.KafkaUrl);

                x.UseDashboard(opt => { opt.PathMatch = ""; });

                //设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
                x.SucceedMessageExpiredAfter = 24 * 3600;

                //设置失败重试次数
                x.FailedRetryCount = 5;
                // 默认分组
                x.DefaultGroup = "DefaultGroup";

            });

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
