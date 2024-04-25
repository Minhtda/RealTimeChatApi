using Application.Common;
using Application.InterfaceService;
using Application.Util;
using Application.ViewModel.UserViewModel;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppConfiguration _appConfiguration;
        private readonly ICurrentTime _currentTime;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper,AppConfiguration appConfiguration,ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appConfiguration = appConfiguration;
            _currentTime = currentTime;
        }

        public async Task<bool> CreateAccount(RegisterModel registerModel)
        {
            var user =await _unitOfWork.UserRepository.FindUserByEmail(registerModel.Email);
            if (user != null) 
            {
                throw new Exception("Email already exist");
            }
            DateTime birthDay;
            if (!DateTime.TryParseExact(registerModel.BirthDay, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDay))
            {
                throw new Exception("Invalid Birthday format. Please use 'yyyy-MM-dd' format.");
            }
            var newAccount = _mapper.Map<User>(registerModel);
            newAccount.BirthDay= birthDay;
            newAccount.RoleId = 4;
            newAccount.PasswordHash = registerModel.Password.Hash();
            (newAccount.FirstName, newAccount.LastName) = StringUtil.SplitName(registerModel.FullName);
            await _unitOfWork.UserRepository.AddAsync(newAccount);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<Token> Login(LoginModel loginModel, string apiOrigin)
        {
            var user = await _unitOfWork.UserRepository.FindUserByEmail(loginModel.Email);
            if (user==null)
            {
                throw new Exception("Email do not exist");
            }
            if(!loginModel.Password.CheckPassword(user.PasswordHash))
            {
                throw new Exception("Password is not correct");
            }
            var findKey = $"{user.Id.ToString()}+{apiOrigin}";
           string? loginData = _unitOfWork.CacheRepository.GetData<string>(findKey);
            if (loginData!=null)
            {
                throw new Exception("You already login");
            }
            var accessToken = user.GenerateTokenString(_appConfiguration!.JWTSecretKey, _currentTime.GetCurrentTime());
            var refreshToken = RefreshToken.GetRefreshToken();
            var key=$"{user.Id.ToString()}+{apiOrigin}";
            var cacheData = _unitOfWork.CacheRepository.SetData<string>(key, refreshToken,_currentTime.GetCurrentTime().AddDays(2));
            return new Token
            {
                accessToken = accessToken,
                refreshToken = refreshToken,
            };
        }
    }
}
