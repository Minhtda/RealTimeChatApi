using Application.InterfaceService;
using Application.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class WebUserController : BaseController
    {
        private readonly IUserService _userService;
        public WebUserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var isCreate = await _userService.CreateAccount(registerModel);
            if (!isCreate)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            string apiOrigin = "Web";
            var newToken = await _userService.Login(loginModel,apiOrigin);
            return Ok(newToken);
        }
        [HttpGet]
        public async Task<IActionResult> SendVerificationCode(string email)
        {
            bool sendSuccess = await _userService.SendVerificationCodeToEmail(email);
            if (sendSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult CheckVerifyCode(string code)
        {
            bool isCorrect = _userService.CheckVerifyCode(code);
            HttpContext.Session.SetString("verifycode", code);
            if (isCorrect)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            string verifycode = HttpContext.Session.GetString("verifycode");
            HttpContext.Session.Clear();
            bool isResetSuccess = await _userService.ResetPassword(verifycode, resetPasswordModel);
            if (isResetSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
