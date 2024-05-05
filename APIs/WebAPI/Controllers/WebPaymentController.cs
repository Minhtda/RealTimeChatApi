using Application.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    public class WebPaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public WebPaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public IActionResult GetPaymentUrl()
        {
            var payemntUrl = _paymentService.GetPayemntUrl();
            if (payemntUrl == null)
            {
                return BadRequest(payemntUrl);
            }
            return Ok(payemntUrl);
        }
    }
}
