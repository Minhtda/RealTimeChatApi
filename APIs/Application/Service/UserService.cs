using Application.Common;
using Application.InterfaceService;
using Application.Util;
using Application.ViewModel.UserViewModel;
using AutoMapper;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Principal;
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
        private readonly ISendMailHelper _sendMailHelper;
        private readonly IClaimService _claimService;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper,AppConfiguration appConfiguration,ICurrentTime currentTime
            ,ISendMailHelper sendMailHelper,IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appConfiguration = appConfiguration;
            _currentTime = currentTime;
            _sendMailHelper = sendMailHelper;
            _claimService = claimService;
        }

        public bool CheckVerifyCode(string key)
        {
            var email=  _unitOfWork.CacheRepository.GetData<string>(key);
            if(email == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CreateAccount(RegisterModel registerModel)
        {
            var user =await _unitOfWork.UserRepository.FindUserByEmail(registerModel.Email);
            if (user != null) 
            {
                throw new Exception("Email already exist");
            }
            DateTime birthDay;
            if (!DateTime.TryParseExact(registerModel.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDay))
            {
                throw new Exception("Invalid Birthday format. Please use 'yyyy-MM-dd' format.");
            }
            var newAccount = _mapper.Map<User>(registerModel);
            newAccount.BirthDay= birthDay;
            newAccount.RoleId = 4;
            newAccount.PasswordHash = registerModel.Password.Hash();
            (newAccount.FirstName, newAccount.LastName) = StringUtil.SplitName(registerModel.Fullname);
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
            var findKey = user.Id.ToString() + "_" + apiOrigin;
           string? loginData = _unitOfWork.CacheRepository.GetData<string>(findKey);
            if (loginData!=null)
            {
                throw new Exception("You already login");
            }
            var accessToken = user.GenerateTokenString(_appConfiguration!.JWTSecretKey, _currentTime.GetCurrentTime());
            var refreshToken = RefreshToken.GetRefreshToken();
            var key=user.Id.ToString()+ "_" + apiOrigin;
            var cacheData = _unitOfWork.CacheRepository.SetData<string>(key, refreshToken,_currentTime.GetCurrentTime().AddDays(2));
            return new Token
            {
                accessToken = accessToken,
                refreshToken = refreshToken,
            };
        }

        public async Task<bool> Logout(string apiOrigin)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(_claimService.GetCurrentUserId);
            string key= user.Id.ToString()+ "_" + apiOrigin;
            bool isDelete= (bool)_unitOfWork.CacheRepository.RemoveData(key);
            return isDelete;
        }

        public async Task<bool> ResetPassword(string code, ResetPasswordModel resetPasswordModel)
        {
            if (resetPasswordModel.Password != resetPasswordModel.ConfirmPassword)
            {
                throw new Exception("Password do not match");
            }
            var email = _unitOfWork.CacheRepository.GetData<string>(code);
            var user = await _unitOfWork.UserRepository.FindUserByEmail(email);
            if (user != null)
            {
                user.PasswordHash = resetPasswordModel.Password.Hash();
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.CacheRepository.RemoveData(code);
            }
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
        public async Task<bool> SendVerificationCodeToEmail(string email)
        {
            var findAccount = await _unitOfWork.UserRepository.FindUserByEmail(email);
            string key;
            if (findAccount == null)
            {
                throw new Exception("Account do not exist");
            }
            try
            {
                key = StringUtil.RandomString(6);
                //Get project's directory and fetch ForgotPasswordTemplate content from EmailTemplate
                string exePath = Environment.CurrentDirectory.ToString();
                string FilePath = exePath + @"/EmailTemplate/ForgotPassword.html";
                StreamReader streamreader = new StreamReader(FilePath);
                string MailText = streamreader.ReadToEnd();
                streamreader.Close();
                //Replace [resetpasswordkey] = key
                MailText = MailText.Replace("[resetpasswordkey]", key);
                //Replace [emailaddress] = email
                MailText = MailText.Replace("[emailaddress]", email);
                var result = await _sendMailHelper.SendMailAsync(email, "ResetPassword", MailText);
                if (!result)
                {
                    return false;
                };

               _unitOfWork.CacheRepository.SetData(key, email, DateTimeOffset.Now.AddMinutes(10));
            }
            catch (Exception ex)
            {
               throw ex;
            }
            return true;
        }
        public async Task<Token> LoginGoogle(string token, string apiOrigin)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(token);
                string email = payload.Email;
                string firstName = payload.GivenName;
                string lastName = payload.FamilyName;
                string pictureUrl = payload.Picture;
                var loginUser = await _unitOfWork.UserRepository.FindUserByEmail(email);
                if (loginUser == null)
                {
                    var newAcc = new User();
                    newAcc.Email = email;
                    newAcc.RoleId = 3;
                    newAcc.IsDelete = false;
                    newAcc.UserName = firstName + " " + lastName;
                    newAcc.FirstName = firstName;
                    newAcc.LastName = lastName;
                    await _unitOfWork.UserRepository.AddAsync(newAcc);
                    await _unitOfWork.SaveChangeAsync();
                    loginUser = await _unitOfWork.UserRepository.FindUserByEmail(email);
                }
                var accessToken = loginUser.GenerateTokenString(_appConfiguration!.JWTSecretKey, _currentTime.GetCurrentTime());
                var refreshToken = RefreshToken.GetRefreshToken();
                var key = loginUser.Id.ToString() + "_" + apiOrigin;
                var cacheData = _unitOfWork.CacheRepository.SetData<string>(key, refreshToken, _currentTime.GetCurrentTime().AddDays(2));
                return new Token
                {
                    accessToken = accessToken,
                    refreshToken = refreshToken,
                };
            }
            catch (InvalidJwtException ex)
            {
                // Token is invalid
                throw new Exception("Invalid token", ex);
            }
            catch (Exception ex)
            {
                // Other exceptions
                throw new Exception("Failed to validate token", ex);
            }
        }
    }
}
