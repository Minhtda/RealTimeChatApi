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
        private readonly IClaimService _claimsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        public PaymentService(IOptions<ZaloPayConfig> zaloPayConfig, IClaimService claimsService,IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            this.zaloPayConfig = zaloPayConfig.Value;
            _claimsService = claimsService;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        public string GetPayemntUrl()
        {
            string paymentUrl = "";
            var zaloPayRequest = new CreateZaloPayRequest(zaloPayConfig.AppId, zaloPayConfig.AppUser, DateTime.UtcNow.GetTimeStamp()
                , 500000, DateTime.UtcNow.ToString("yyMMdd") + "_" + _claimsService.GetCurrentUserId.ToString(), "zalopayapp", "ZaloPay demo");
            zaloPayRequest.MakeSignature(zaloPayConfig.Key1);
            (bool createZaloPayLinkResult, string? createZaloPayMessage) = zaloPayRequest.GetLink(zaloPayConfig.PaymentUrl);
            if (createZaloPayLinkResult)
            {
                _cacheService.SetData<string>(_claimsService.GetCurrentUserId.ToString(), zaloPayRequest.AppTransId, DateTimeOffset.UtcNow.AddHours(20));
                paymentUrl = createZaloPayMessage;
            }
            return paymentUrl;
        }
        public int ReturnTransactionStatus()
        {
            int status = 0;
            string apptransid = _cacheService.GetData<string>(_claimsService.GetCurrentUserId.ToString());
            var zaloPayRequest = new CreateZaloPayRequest(zaloPayConfig.AppId, null, 0, 0, apptransid, null, null);
            zaloPayRequest.MakeSignatureForAppTransStatus(zaloPayConfig.Key1);
            (int appTransStatus, string? appTransMessage) = zaloPayRequest.GetStatus(zaloPayConfig.AppTransStatusUrl);
            if (appTransStatus != 0)
            {
                status = appTransStatus;
                return status;
            }
            return status;
        }
    }
}
