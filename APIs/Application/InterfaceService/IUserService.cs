using Application.ViewModel.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceService
{
    public interface IUserService
    {
        Task<bool> CreateAccount(RegisterModel registerModel);
        Task<Token> Login (LoginModel loginModel,string apiOrigin);
        Task<bool> SendVerificationCodeToEmail(string email);
        bool CheckVerifyCode(string key);
        Task<bool> ResetPassword(string code,ResetPasswordModel resetPasswordModel);
        Task<bool> Logout(string apiOrigin);
        Task<Token> LoginGoogle(string token, string apiOrigin);
    }
}
