using Application.InterfaceRepository;
using Application.InterfaceService;
using Backend.Domain.Test;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileAPI;
using MobileAPI.MobileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Backend.Infratructure.Test
{
    public class DependencyInjectionTest:SetupTest
    {
        private readonly IServiceProvider _serviceProvider;
        public DependencyInjectionTest()
        {
            var service=new ServiceCollection();
            service.AddInfrastructureService("Testing","TestCache");
            service.AddDbContext<AppDbContext>(
               option => option.UseInMemoryDatabase("test"));
            service.AddMobileAPIService("Test");
            service.AddWebAPIService("Test");   
            _serviceProvider = service.BuildServiceProvider();
        }
        [Fact]
        public void GetService_ShouldRetunCorrectType()
        {
            var userRepositoryResolved = _serviceProvider.GetRequiredService<IUserRepository>();
            userRepositoryResolved.GetType().Should().Be(typeof(UserRepository));
        }
    }
}
