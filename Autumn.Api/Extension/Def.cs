using Autumn.Common;
using System.Configuration;

namespace Autumn.Extension
{
    /// <summary>
    /// 常量定义
    /// </summary>
    public static class Def
    {
        /// <summary>
        /// Api名称
        /// </summary>
        public const string ApiName = "Autumn.Api";

        /// <summary>
        /// 日志配置名称
        /// </summary>
        public const string NLogConfigName = "NLog.config";

        /// <summary>
        /// 站点全称
        /// </summary>
        public const string HtmlName = ".index.html";

        /// <summary>
        /// 跨域名称
        /// </summary>
        public const string CorsName = "LimitRequests";

        #region Alipay
        public const string AlipayAppId = "2016081600256163";
        public const string AlipayNotifyUrl = "http://localhost:8081/Notify";
        public const string AlipayReturnUrl = "http://localhost:8081/Notify";
        public const string AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsW6+mN2E3Oji2DPjSKuYgRzK6MlH9q6" +
                                              "W0iM0Yk3R0qbpp5wSesSXqudr2K25gIBOTCchiIbXO7GXt/zEdnhnC32eOaTnonDsnuBWIp+q7LoV" +
                                              "x/gvKIX5LTHistCvGli8VW4EDGsu2jAyQXyMPgPrIz+/NzWis/gZsa4TaqVY4SpWRuSgMXxleh2ERB6" +
                                              "k0ijK0IYM+Cv5fz1ZPDCgk7EbII2jk2fDxtlMLoN5UYEJCcD8OUyivm3Hti3u1kPolckCCf0xk+80g/4" +
                                              "EdmzFAffsVgPeXZrkm5EIuiTTOIeRHXlTg3HtkkCw2Wl0CpYSKBr9Vzv7x0gNvb1wnXPmBJNRgQIDAQAB";
        public const string AlipayPrivatekey = "MIIEpAIBAAKCAQEAyC43UbsE5XZ2Pmqg1YgzeCqAMk4HOH8fYHslseeSgKxyDjybjqM0yjGIJry1FRmV" +
                                               "vLnY7v8jURgwr7d/pDCSRdoHa6zaxuSzg0OlieNmujae34YZ54PmFxULZW0BHSdzmx3OIYK2GarRECkds" +
                                               "531ZzpbLdRXqsxQf5G26JZLIFxmNuh/VjBjJ6Hic1WOFT+FCYyi8om+LkPn3jELeA7LPLXzFqzzxx0vo4y" +
                                               "iAePrsX5WucWxf+Y8rZoDhRIy/cPtQECXi9SiAWOJe/82JqjVjfpowf3QN7UJHsA82RBloAS4lvvDGJA7" +
                                               "a+8DDlqpqPer8cS41Dv5r39iqtJUybDqoQIDAQABAoIBAHi39kBhiihe8hvd7bQX+QIEj17G02/sqZ1jZm" +
                                               "4M+rqCRB31ytGP9qvghvzlXEanMTeo0/v8/O1Qqzusa1s2t19MhqEWkrDTBraoOtIWwsKVYeXmVwTY9A8" +
                                               "Db+XwgHV2by8iIEbxLqP38S/Pu8uv/GgONyJCJcQohnsIAsfsqs2OGggz+PplZaXJfUkPomWkRdHM9ZWW" +
                                               "DLrCIlmRSHLmhHEtFJaXD083kqo437qra58Amw/n+2gH57utbAQ9V3YQFjD8zW511prC+mB6N/WUlaLst" +
                                               "kxswGJ16obEJfQ0r8wYHx14ep6UKGyi3YXlMHcteI8gz+uFx4RuVV9EotdXagECgYEA7AEz9oPFYlW1H15O" +
                                               "kDGy8yBnpJwIBu2CQLxINsxhrLIAZ2Bgxqcsv+D9CpnYCBDisbXoGoyMK6XaSypBMRKe2y8yRv4c+w00r" +
                                               "cKHtGfRjzSJ5NQO0Tv+q8vKY+cd6BuJ6OUQw82ICLANIfHJZNxtvtTCmmqBwSJDpcQJQXmKXTECgYEA2SQ" +
                                               "CSBWZZONkvhdJ15K+4IHP2HRbYWi+C1OvKzUiK5bdJm77zia4yJEJo5Y/sY3mV3OK0Bgb7IAaxL3i0oH+WN" +
                                               "TwbNoGpMlYHKuj4x1453ITyjOwPNj6g27FG1YSIDzhB6ZC4dBlkehi/7gIlIiQt1wkIZ+ltOqgI5IqIdXo" +
                                               "SHECgYB3zCiHYt4oC1+UW7e/hCrVNUbHDRkaAygSGkEB5/9QvU5tK0QUsrmJcPihj/RUK9YW5UK7b0qbw" +
                                               "WWsr/dFpLEUi8GWvdkSKuLprQxbrDN44O96Q5Z96Vld9WV4DtJkhs4bdWNsMQFzf4I7D9PuKeJfcvqRjazt" +
                                               "z6nNFFSqcrqkkQKBgQCJKlUCohpG/9notp9fvQQ0n+viyQXcj6TVVOSnf6X5MRC8MYmBHTbHA8+59bSAfa" +
                                               "nO/l7muwQQro+6TlUVMyaviLvjlwpxV/sACXC6jCiO06IqreIbXdlJ41RBw2op0Ss5gM5pBRLUS58V+HP7GB" +
                                               "WKrnrofofXtAq6zZ8txok4EQKBgQCXrTeGMs7ECfehLz64qZtPkiQbNwupg938Z40Qru/G1GR9u0kmN7ibT" +
                                               "yYauI6NNVHGEZa373EBEkacfN+kkkLQMs1tj5Zrlw+iITm+ad/irpXQZS/NHCcrg6h82vu0LcgiKnHKlmW" +
                                               "6K5ne0w4LqmsmRCm7JdJjt9WlapAs0ticiw==";
        public const string AlipayGatewayUrl = "https://openapi.alipaydev.com";
        #endregion

        #region Wechatpay
        public const string WechatpayAppId = "wx2428e34e0e7dc6ef";
        public const string WechatpayMchId = "1233410002";
        public const string WechatpayKey = "e10adc3849ba56abbe56e056f20f883e";
        public const string WechatpayAppSecret = "51c56b886b5be869567dd389b3e5d1d6";
        public const string WechatpaySslCertPath = "Certs/apiclient_cert.p12";
        public const string WechatpaySslCertPassword = "1233410002";
        public const string WechatpayNotifyUrl = "http://localhost:8081/Notify";
        #endregion

        /// <summary>
        /// 发行人
        /// </summary>
        public static readonly string Issuer = Appsettings.Get(new string[] { "Audience", "Issuer" });

        /// <summary>
        /// 订阅人
        /// </summary>
        public static readonly string Audience = Appsettings.Get(new string[] { "Audience", "Audience" });

        /// <summary>
        /// 密钥
        /// </summary>
        public static readonly string Secret = Appsettings.Get(new string[] { "Audience", "Secret" });

        /// <summary>
        /// Cookie是否使用
        /// </summary>
        public static readonly bool CookieFlg = Appsettings.Get(new string[] { "Cookie", "Enabled" }).ToBool();

        /// <summary>
        /// Session是否使用
        /// </summary>
        public static readonly bool SessionFlg = Appsettings.Get(new string[] {"Session", "Enabled" }).ToBool();

        /// <summary>
        /// Redis缓存是否使用
        /// </summary>
        public static readonly bool RedisCachingFlg = Appsettings.Get(new string[] { "AppSettings", "RedisCaching", "Enabled" }).ToBool();

        /// <summary>
        /// Redis缓存切片地址
        /// </summary>
        public static readonly string RedisCachingUrl = Appsettings.Get(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });

        /// <summary>
        /// 系统缓存切片状态
        /// </summary>
        public static readonly bool MemoryCachingFlg = Appsettings.Get(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ToBool();

        /// <summary>
        /// 日志切片状态
        /// </summary>
        public static readonly bool LogFlg = Appsettings.Get(new string[] { "AppSettings", "LogAOP", "Enabled" }).ToBool();

        /// <summary>
        /// 跨域状态
        /// </summary>
        public static readonly bool AllowedCrosFlg = Appsettings.Get(new string[] { "AllowedCros", "Enabled" }).ToBool();

        /// <summary>
        /// 跨域地址
        /// </summary>
        public static readonly string AllowedCrosOrigins = Appsettings.Get(new string[] { "AllowedCros", "WithOrigins" });

        /// <summary>
        /// 请求响应中间件
        /// </summary>
        public static readonly bool RequestResponseMiddleware = Appsettings.Get(new string[] { "Middleware_RequestResponse", "Enabled" }).ToBool();

        /// <summary>
        /// Jwt认证中间件
        /// </summary>
        public static readonly bool JwtAuthMiddleware = Appsettings.Get(new string[] { "Middleware_JwtAuth", "Enabled" }).ToBool();

        /// <summary>
        /// Jwt认证过期时间（秒）
        /// </summary>
        public static readonly int JwtAuthMiddleware_Exp = Appsettings.Get(new string[] { "Middleware_JwtAuth", "Exp" }).ToInt();

        /// <summary>
        /// SignalR
        /// </summary>
        public static readonly bool SignalrFlg = Appsettings.Get(new string[] { "SignalR", "Enabled" }).ToBool();

        /// <summary>
        /// PaymentGateway
        /// </summary>
        public static readonly bool PaymentGatewayFlg = Appsettings.Get(new string[] { "PaymentGateway", "Enabled" }).ToBool();

        /// <summary>
        /// 防御跨站请求伪造攻击
        /// </summary>
        public static readonly bool CsrfFlg = Appsettings.Get(new string[] { "Csrf", "Enabled" }).ToBool();
    }
}
