using Application.InterfaceService;
using Application.Util;
using Application.ZaloPay.Config;
using Application.ZaloPay.Request;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
namespace Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly ZaloPayConfig zaloPayConfig;
        public PaymentService(IOptions<ZaloPayConfig> zaloPayConfig)
        {
            this.zaloPayConfig = zaloPayConfig.Value;
        }
        public string GetPayemntUrl()
        {
            string paymentUrl="";
            var zaloPayRequest = new CreateZaloPayRequest(zaloPayConfig.AppId,zaloPayConfig.AppUser,DateTime.UtcNow.GetTimeStamp()
                , 50000, DateTime.UtcNow.ToString("yyMMdd") + "_" +Guid.NewGuid().ToString(), "zalopayapp", "ZaloPay demo");
            zaloPayRequest.MakeSignature(zaloPayConfig.Key1);
            (bool createZaloPayLinkResult, string? createZaloPayMessage) = zaloPayRequest.GetLink(zaloPayConfig.PaymentUrl);
            if (createZaloPayLinkResult)
            {
                paymentUrl = createZaloPayMessage;
            }
            return paymentUrl;
        }
    }
}
