using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using AspNetCoreRateLimit;
using NLog.Web;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using PaySharp.Alipay;
using PaySharp.Wechatpay;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using StackExchange.Profiling.Storage;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using static Autumn.SwaggerHelper.CustomApiVersion;
using Autumn.AOP;
using Autumn.SignalrChat;
using Autumn.AuthHelper;
using Autumn.Common;
using Autumn.Common.MemoryCache;
using Autumn.Extension;
using Autumn.Filters;
using Autumn.FrameWork;
using Autumn.Middlewares;
using Autumn.Attributes;
using Autumn.Api.Attributes;
using EasyOffice;

namespace Autumn
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 服务注册
            //添加appsettings.json
            services.AddOptions();

            // Controller As Service
            services.AddMvc().AddControllersAsServices().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // MemoryCache
            services.AddScoped<ICaching, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });

            // Redis
            services.AddSingleton<IRedisHelper, RedisHelper>();
            // Context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            // Session
            services.AddSession();

            #endregion

            #region Office
            services.AddEasyOffice(new OfficeOptions());
            #endregion

            #region IpRateLimitOptions

            //需要存储速率和ip规则
            services.AddMemoryCache();
            //加载配置项
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            //单机计时器和规则
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            //分布式缓存计时器
            //services.AddSingleton<IClientPolicyStore, DistributedCacheClientPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();

            #endregion

            #region 初始化
            // DB
            services.AddScoped<DbContext>();
            // Mapper
            services.AddAutoMapper(typeof(Startup));

            #endregion

            #region AllowCors

            services.AddCors(c =>
            {
                if (Def.AllowedCrosFlg)
                {
                    c.AddPolicy(Def.CorsName, policy =>
                    {
                        policy
                            .WithOrigins(Def.AllowedCrosOrigins.Split(","))
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
                }
                else
                {
                    c.AddPolicy(Def.CorsName, policy =>
                    {
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
                }
            });
            #endregion

            #region 服务监控

            services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                    (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
                }
            );

            #endregion

            #region 接口文档

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        // {ApiName} 定义成全局变量
                        Version = version,
                        Title = $"{Def.ApiName} 接口文档",
                        Description = $"{Def.ApiName} " + version,
                        TermsOfService = "None",
                        Contact = new Contact { Name = Def.ApiName, Email = "Autumn@xxx.com", Url = "https://www.hydsoft.com/" }
                    });
                    // 按相对路径排序
                    c.OrderActionsBy(o => o.RelativePath);
                });
                //解决相同类名会报错的问题
                c.CustomSchemaIds(type => type.FullName);
                //配置的xml文件名
                var xmlPath = Path.Combine(basePath, Def.ApiName + ".xml");
                c.IncludeXmlComments(xmlPath, true);
                //这个就是Model层的xml文件名
                var xmlModelPath = Path.Combine(basePath, "Autumn.Model.xml");
                c.IncludeXmlComments(xmlModelPath);

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                // 发行人
                var IssuerName = Def.Issuer;
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                c.AddSecurityRequirement(security);

                //方案名称“Autumn”可自定义，上下一致即可
                c.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "请在下框中输入Bearer {token}",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion
            });

            #endregion

            #region 全局异常捕获

            //注入全局异常捕获
            services.AddMvc(o =>
            {
                // 模型有效认证
                o.Filters.Add<ModelValidationAttribute>();
                // 授权许可
                o.Filters.Add<PermissionAttribute>();
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            // 取消默认驼峰
            .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

            #endregion

            #region SignalR
            if (Def.SignalrFlg)
                services.AddSignalR();
            #endregion

            #region HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            #region 策略授权
            // 第一步：配置【策略授权】
            // 第二步：配置【统一认证】
            // 第三步：开启中间件app.UseAuthentication()
            #region 参数
            // 读取配置文件
            var symmetricKeyAsBase64 = Def.Secret;
            // 发行人
            var symIssuer = Def.Issuer;
            // 订阅人
            var symAudience = Def.Audience;

            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);

            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            #endregion

            #endregion

            #region 统一认证
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = symIssuer,
                ValidateAudience = true,
                ValidAudience = symAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            // JWT认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             });

            #endregion

            #region AutoFac DI
            //实例化容器 
            var builder = new ContainerBuilder();
            builder.RegisterType<HydCacheAOP>();
            builder.RegisterType<HydRedisCacheAOP>();
            builder.RegisterType<HydLogAOP>();

            #region 带有接口层的服务注入

            #region 服务切片注入

            //获取项目绝对路径
            var servicesDllFile = Path.Combine(basePath, "Autumn.Services.dll");
            //加载文件
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            // 切片开关
            var cacheType = new List<Type>();
            if (Def.RedisCachingFlg)
            {
                cacheType.Add(typeof(HydRedisCacheAOP));
            }
            if (Def.MemoryCachingFlg)
            {
                cacheType.Add(typeof(HydCacheAOP));
            }
            if (Def.LogFlg)
            {
                cacheType.Add(typeof(HydLogAOP));
            }

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()// 引用Autofac.Extras.DynamicProxy
                      .InterceptedBy(cacheType.ToArray());// 允许将拦截器服务的列表分配给注册
            #endregion

            #region Repository.dll 注入，有对应接口
            var repositoryDllFile = Path.Combine(basePath, "Autumn.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            #endregion

            #endregion

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件创建新容器
            var ApplicationContainer = builder.Build();

            #endregion

            #region Payment Gateway

            services.AddWebEncoders(opt =>
            {
                opt.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddPaySharp(a =>
            {
                var alipayMerchant = new PaySharp.Alipay.Merchant
                {
                    AppId = Def.AlipayAppId,
                    NotifyUrl = Def.AlipayNotifyUrl,
                    ReturnUrl = Def.AlipayReturnUrl,
                    AlipayPublicKey = Def.AlipayPublicKey,
                    Privatekey = Def.AlipayPrivatekey
                };

                var wechatpayMerchant = new PaySharp.Wechatpay.Merchant
                {
                    AppId = Def.WechatpayAppId,
                    MchId = Def.WechatpayMchId,
                    Key = Def.WechatpayKey,
                    AppSecret = Def.WechatpayAppSecret,
                    SslCertPath = Def.WechatpaySslCertPath,
                    SslCertPassword = Def.WechatpaySslCertPassword,
                    NotifyUrl = Def.WechatpayNotifyUrl
                };

                a.Add(new AlipayGateway(alipayMerchant)
                {
                    GatewayUrl = Def.AlipayGatewayUrl
                });

                a.Add(new WechatpayGateway(wechatpayMerchant));

                a.UseAlipay(Configuration);

                a.UseWechatpay(Configuration);
            });

            #endregion

            #region Csrf
            if (Def.CsrfFlg)
                services.AddAntiforgery(options => { options.Cookie.SameSite = SameSiteMode.Lax; options.HeaderName = "X-XSRF-TOKEN"; });
            #endregion

            //IOC接管内置DI
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
        {
            #region Environment
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();
            }
            #endregion
            
            #region HttpContext
            HttpAccessor._Accessor = accessor;
            #endregion

            #region IpRateLimitOptions
            app.UseIpRateLimiting();

            #endregion

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //根据版本名称正序 遍历展示
                typeof(ApiVersions).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{Def.ApiName} {version}");
                });
                // 将swagger首页,设置成我们自定义的页面:解决方案名+.index.html
                // 这里是配合MiniProfiler进行性能监控的
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream(Def.ApiName + Def.HtmlName);
                // 路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉
                c.RoutePrefix = ""; 
                // Display
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.ShowExtensions();
            });

            #endregion

            #region MiniProfiler
            app.UseMiniProfiler();
            #endregion

            #region Authen
            app.UseAuthentication();
            #endregion

            #region CORS
            if (Def.AllowedCrosFlg) app.UseCors(Def.CorsName);
            #endregion

            #region WebSite
            // 请求与返回
            if (Def.RequestResponseMiddleware) app.UseReuestResponseLog();
            // JWT认证
            if (Def.JwtAuthMiddleware) app.UseJwtTokenAuth();
            // 重定向
            //app.UseHttpsRedirection();
            // 静态文件
            app.UseStaticFiles();
            // Cookie
            if (Def.CookieFlg) app.UseCookiePolicy();
            // Session
            if (Def.SessionFlg) app.UseSession();
            // 返回错误码
            app.UseStatusCodePages();

            #endregion

            #region NLog
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loggerFactory.AddNLog();
            env.ConfigureNLog(Def.NLogConfigName);
            #endregion

            #region SignalR
            if (Def.SignalrFlg)
            {
                app.UseSignalR(routes =>
                {
                    //如果你不用/api/xxx的这个规则的话，会出现跨域问题
                    routes.MapHub<ChatHub>("/ChatHub");
                });
            }
            #endregion

            #region Mvc
            app.UseMvc();
            #endregion

            #region Payment Gateway
            if (Def.PaymentGatewayFlg) app.UsePaySharp();
            #endregion
        }
    }
}
