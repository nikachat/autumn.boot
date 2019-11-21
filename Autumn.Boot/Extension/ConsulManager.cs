using Autumn.Model;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Autumn.Api.Extension
{
    /// <summary>
    /// 服务治理
    /// </summary>
    public static class ConsulManager
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <param name="healthService"></param>
        /// <param name="consulService"></param>
        /// <returns></returns>
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, HealthModel healthService, ConsulModel consulService)
        {
            // 请求注册的 Consul 地址
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{consulService.Ip}:{consulService.Port}"));
            var httpCheck = new AgentServiceCheck()
            {
                // 服务启动多久后注册
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                // 健康检查时间间隔
                Interval = TimeSpan.FromSeconds(10),
                // 健康检查地址
                HTTP = $"http://{healthService.Ip}:{healthService.Port}/api/health/get",
                Timeout = TimeSpan.FromSeconds(5)
            };

            // 注册
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = healthService.Name + "_" + healthService.Port,
                Name = healthService.Name,
                Address = healthService.Ip,
                Port = healthService.Port,
                // 添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
                Tags = new[] { $"urlprefix-/{healthService.Name}" }
            };
            // 服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            consulClient.Agent.ServiceRegister(registration).Wait();
            lifetime.ApplicationStopping.Register(() =>
            {
                //服务停止时取消注册
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });
            return app;
        }
    }
}
