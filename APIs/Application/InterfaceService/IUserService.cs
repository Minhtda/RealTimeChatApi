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
    }
}
