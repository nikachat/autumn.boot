using System;
using Autumn.Controllers;
using Microsoft.AspNetCore.Mvc;
using PaySharp.Core;
using PaySharp.Core.Response;
using PaySharp.Wechatpay;
using PaySharp.Wechatpay.Domain;
using PaySharp.Wechatpay.Request;

namespace Autumn.Api.Controllers
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WechatpayController : BaseController
    {
        private readonly IGateway _gateway;

        public WechatpayController(IGateways gateways)
        {
            _gateway = gateways.Get<WechatpayGateway>();
        }

        /// <summary>
        /// 公钥
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PublicKey()
        {
            var request = new PublicKeyRequest();

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OAuth(string code)
        {
            var request = new OAuthRequest();
            request.AddGatewayData(new OAuthModel()
            {
                Code = code
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 公众号支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="open_id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PublicPay(string out_trade_no, int total_amount, string body, string open_id)
        {
            var request = new PublicPayRequest();
            request.AddGatewayData(new PublicPayModel()
            {
                Body = body,
                OutTradeNo = out_trade_no,
                TotalAmount = total_amount,
                OpenId = open_id
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 移动支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AppPay(string out_trade_no, int total_amount, string body)
        {
            var request = new AppPayRequest();
            request.AddGatewayData(new AppPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 小程序支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="open_id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AppletPay(string out_trade_no, int total_amount, string body, string open_id)
        {
            var request = new AppletPayRequest();
            request.AddGatewayData(new AppletPayModel()
            {
                Body = body,
                OutTradeNo = out_trade_no,
                TotalAmount = total_amount,
                OpenId = open_id
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 手机网站支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <param name="scene_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WapPay(string out_trade_no, int total_amount, string body, string scene_info)
        {
            var request = new WapPayRequest();
            request.AddGatewayData(new WapPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                SceneInfo = scene_info
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 扫码支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="body"></param>
        /// <param name="total_amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ScanPay(string out_trade_no, string body, int total_amount)
        {
            var request = new ScanPayRequest();
            request.AddGatewayData(new ScanPayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);

            return Json(response);
        }

        /// <summary>
        /// 条码支付
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="auth_code"></param>
        /// <param name="total_amount"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BarcodePay(string out_trade_no, string auth_code, int total_amount, string body)
        {
            var request = new BarcodePayRequest();
            request.AddGatewayData(new BarcodePayModel()
            {
                Body = body,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no,
                AuthCode = auth_code
            });
            request.PaySucceed += BarcodePay_PaySucceed;
            request.PayFailed += BarcodePay_PayFaild;

            var response = _gateway.Execute(request);

            return Json(response);
        }

        /// <summary>
        /// 支付成功事件
        /// </summary>
        /// <param name="response">返回结果</param>
        /// <param name="message">提示信息</param>
        private void BarcodePay_PaySucceed(IResponse response, string message)
        {
        }

        /// <summary>
        /// 支付失败事件
        /// </summary>
        /// <param name="response">返回结果,可能是BarcodePayResponse/QueryResponse</param>
        /// <param name="message">提示信息</param>
        private void BarcodePay_PayFaild(IResponse response, string message)
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Query(string out_trade_no, string trade_no)
        {
            var request = new QueryRequest();
            request.AddGatewayData(new QueryModel()
            {
                TradeNo = trade_no,
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <param name="total_amount"></param>
        /// <param name="refund_amount"></param>
        /// <param name="refund_desc"></param>
        /// <param name="out_refund_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Refund(string out_trade_no, string trade_no, int total_amount, int refund_amount, string refund_desc, string out_refund_no)
        {
            var request = new RefundRequest();
            request.AddGatewayData(new RefundModel()
            {
                TradeNo = trade_no,
                RefundAmount = refund_amount,
                RefundDesc = refund_desc,
                OutRefundNo = out_refund_no,
                TotalAmount = total_amount,
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 退款查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="trade_no"></param>
        /// <param name="out_refund_no"></param>
        /// <param name="refund_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RefundQuery(string out_trade_no, string trade_no, string out_refund_no, string refund_no)
        {
            var request = new RefundQueryRequest();
            request.AddGatewayData(new RefundQueryModel()
            {
                TradeNo = trade_no,
                OutTradeNo = out_trade_no,
                OutRefundNo = out_refund_no,
                RefundNo = refund_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Close(string out_trade_no)
        {
            var request = new CloseRequest();
            request.AddGatewayData(new CloseModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cancel(string out_trade_no)
        {
            var request = new CancelRequest();
            request.AddGatewayData(new CancelModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="openid"></param>
        /// <param name="check_name"></param>
        /// <param name="true_name"></param>
        /// <param name="amount"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Transfer(string out_trade_no, string openid, string check_name, string true_name, int amount, string desc)
        {
            var request = new TransferRequest();
            request.AddGatewayData(new TransferModel()
            {
                OutTradeNo = out_trade_no,
                OpenId = openid,
                Amount = amount,
                Desc = desc,
                CheckName = check_name,
                TrueName = true_name
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 转账查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TransferQuery(string out_trade_no)
        {
            var request = new TransferQueryRequest();
            request.AddGatewayData(new TransferQueryModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 转账到银行卡
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="bank_no"></param>
        /// <param name="true_name"></param>
        /// <param name="bank_code"></param>
        /// <param name="amount"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TransferToBank(string out_trade_no, string bank_no, string true_name, string bank_code, int amount, string desc)
        {
            var request = new TransferToBankRequest();
            request.AddGatewayData(new TransferToBankModel()
            {
                OutTradeNo = out_trade_no,
                BankNo = bank_no,
                Amount = amount,
                Desc = desc,
                BankCode = bank_code,
                TrueName = true_name
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 转账到银行卡查询
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TransferToBankQuery(string out_trade_no)
        {
            var request = new TransferToBankQueryRequest();
            request.AddGatewayData(new TransferToBankQueryModel()
            {
                OutTradeNo = out_trade_no
            });

            var response = _gateway.Execute(request);
            return Json(response);
        }

        /// <summary>
        /// 对账单下载
        /// </summary>
        /// <param name="bill_date"></param>
        /// <param name="bill_type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BillDownload(string bill_date, string bill_type)
        {
            var request = new BillDownloadRequest();
            request.AddGatewayData(new BillDownloadModel()
            {
                BillDate = bill_date,
                BillType = bill_type
            });

            var response = _gateway.Execute(request);
            return File(response.GetBillFile(), "text/csv", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");
        }

        /// <summary>
        /// 资金账单下载
        /// </summary>
        /// <param name="bill_date"></param>
        /// <param name="account_type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FundFlowDownload(string bill_date, string account_type)
        {
            var request = new FundFlowDownloadRequest();
            request.AddGatewayData(new FundFlowDownloadModel()
            {
                BillDate = bill_date,
                AccountType = account_type
            });

            var response = _gateway.Execute(request);
            return File(response.GetBillFile(), "text/csv", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");
        }
    }
}