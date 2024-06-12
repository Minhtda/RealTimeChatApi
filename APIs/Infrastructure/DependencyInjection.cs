﻿using Application;
using Application.InterfaceRepository;
using Application.InterfaceService;
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
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,string databaseConnectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(databaseConnectionString).EnableSensitiveDataLogging());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IVerifyUsersRepository, VerifyUsersRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;    
        }
    }
}
