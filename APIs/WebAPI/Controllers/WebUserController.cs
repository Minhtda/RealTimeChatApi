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
    }
}
