using Application;
using Application.InterfaceRepository;
using Application.InterfaceService;
using Infrastructure.Cache;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,string databaseConnectionString,string cacheConnectionString)
        {
            var options = ConfigurationOptions.Parse(cacheConnectionString); // host1:port1, host2:port2, ...
            options.Password = "MinhQuan@123";
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(databaseConnectionString).EnableSensitiveDataLogging());
            services.AddScoped<IDatabase>(cfg =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(options);
                return multiplexer.GetDatabase();
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICacheRepository,CacheRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;    
        }
    }
}
