using Application.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileAPI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetPaymentUrl() 
        {
            var payemntUrl= _paymentService.GetPayemntUrl();
            if (payemntUrl == null)
            {
                return BadRequest(payemntUrl);
            }
            return Ok(payemntUrl);
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetPaymentStatus()
        {
            var paymentStatus = _paymentService.ReturnTransactionStatus();
            if (paymentStatus > 0)
            {
                return Ok(paymentStatus);
            }
            return BadRequest(paymentStatus);
        }
    }
}
