using Application.InterfaceService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MobileAPI.MobileService;
using Application.Service;
using Application.ZaloPay.Config;
using Microsoft.Extensions.DependencyInjection;
using Application.Util;
namespace MobileAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMobileAPIService(this IServiceCollection services,string secretKey) 
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ICurrentTime, CurrentTime>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();  
            services.AddScoped<ISendMailHelper, SendMailHelper>();  
            services.AddScoped<IPostService, PostService>();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = secretKey,
                      ValidAudience = secretKey,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                      ClockSkew = TimeSpan.FromSeconds(1)
                  };
              });
           
            return services;
        }
    }
}
