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
using Domain.Entities;
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

        public async Task<bool> AddMoneyToWallet()
        {
            int statusCode = ReturnTransactionStatus();
            string key=_claimsService.ToString()+"_"+"Payment";
            Wallet foundWallet = await _unitOfWork.WalletRepository.FindWalletByUserId(_claimsService.GetCurrentUserId);
            string apptransid = _cacheService.GetData<string>(_claimsService.GetCurrentUserId.ToString());
            long amount = _cacheService.GetData<long>(apptransid);
            if (statusCode > 0)
            {
                if (foundWallet == null)
                {
                    Wallet wallet = new Wallet()
                    {
                        OwnerId = _claimsService.GetCurrentUserId,
                        UserBalance = amount,
                    };
                    _cacheService.RemoveData(_claimsService.GetCurrentUserId.ToString());
                    await _unitOfWork.WalletRepository.AddAsync(wallet);
                }
                else
                {
                    foundWallet.UserBalance += amount;
                    _unitOfWork.WalletRepository.Update(foundWallet);
                }

            }
            else
            {
                throw new Exception("Update user balance error");
            }
            return await _unitOfWork.SaveChangeAsync()>0;
        }

        public string GetPayemntUrl()
        {
            string paymentUrl = "";
            long amount=50000;
            string key=_claimsService.GetCurrentUserId.ToString()+"_"+"Payment";
            var zaloPayRequest = new CreateZaloPayRequest(zaloPayConfig.AppId, zaloPayConfig.AppUser, DateTime.UtcNow.GetTimeStamp()
                , amount, DateTime.UtcNow.ToString("yyMMdd") + "_" + _claimsService.GetCurrentUserId.ToString(), "zalopayapp", "ZaloPay demo");
            zaloPayRequest.MakeSignature(zaloPayConfig.Key1);
            (bool createZaloPayLinkResult, string? createZaloPayMessage) = zaloPayRequest.GetLink(zaloPayConfig.PaymentUrl);
            if (createZaloPayLinkResult)
            {
                _cacheService.SetData<string>(key, zaloPayRequest.AppTransId, DateTimeOffset.UtcNow.AddHours(20));
                _cacheService.SetData<long>(zaloPayRequest.AppTransId, amount, DateTimeOffset.UtcNow.AddDays(2));
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
