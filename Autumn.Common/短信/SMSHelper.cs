using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using System;

namespace Autumn.Common
{
    /// <summary>
    /// 短信发送帮助类
    /// </summary>
    public class SMSHelper
    {
        /// <summary>
        /// 阿里云
        /// </summary>
        /// <param name="sMSModel"></param>
        public static HttpResponse AliyunSend(SMSModel sMSModel)
        {
            if (string.IsNullOrEmpty(sMSModel.AccessKeyId))
                throw new Exception("主账号AccessKey的ID不能为空.");

            if (string.IsNullOrEmpty(sMSModel.AccessSecret))
                throw new Exception("主账号AccessSecret不能为空.");

            if (string.IsNullOrEmpty(sMSModel.PhoneNumbers))
                throw new Exception("接收短信的手机号码不能为空.");

            if (string.IsNullOrEmpty(sMSModel.SignName))
                throw new Exception("短信签名名称不能为空.");

            if (string.IsNullOrEmpty(sMSModel.TemplateCode))
                throw new Exception("短信模板ID不能为空.");

            IClientProfile profile = DefaultProfile.GetProfile(sMSModel.RegionId, sMSModel.AccessKeyId, sMSModel.AccessSecret);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            // 短信API产品域名
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", sMSModel.PhoneNumbers);
            request.AddQueryParameters("SignName", sMSModel.SignName);
            request.AddQueryParameters("TemplateCode", sMSModel.TemplateCode);
            request.AddQueryParameters("TemplateParam", sMSModel.TemplateParam);
            request.AddQueryParameters("SmsUpExtendCode", sMSModel.SmsUpExtendCode);
            request.AddQueryParameters("OutId", sMSModel.OutId);

            try
            {
                CommonResponse response = client.GetCommonResponse(request);

                return response.HttpResponse;
            }
            catch (ServerException e)
            {
                throw e;
            }
            catch (ClientException e)
            {
                throw e;
            }
        }
    }
}
