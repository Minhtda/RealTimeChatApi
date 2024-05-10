using Application;
using Application.Common;
using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.Util;
using AutoFixture;
using AutoMapper;
using Infrastructure;
using Infrastructure.Cache;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Backend.Domain.Test
{
    public class SetupTest : IDisposable
    {
        protected readonly AppDbContext _dbContext;
        protected readonly Fixture _fixture;
        protected readonly IMapper _mapper;
        protected readonly Mock<IUserRepository> _userRepositoryMock;
        protected readonly Mock<IClaimService> _claimServiceMock;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<ICurrentTime> _currentTimeMock;
        protected readonly Mock<ICacheRepository> _cacheRepositoryMock;
        protected readonly Mock<ISendMailHelper> _sendMailHelperMock;
        protected readonly Mock<AppConfiguration> _appConfiguration;
        public SetupTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString())
               .Options;
            _dbContext = new AppDbContext(options);
            var mapConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());
            });
            _mapper=mapConfig.CreateMapper();
            _fixture=new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _claimServiceMock = new Mock<IClaimService>();
            _currentTimeMock=new Mock<ICurrentTime>();
            _cacheRepositoryMock=new Mock<ICacheRepository>();
            _appConfiguration = new Mock<AppConfiguration>();
            _claimServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.Empty);
            _currentTimeMock.Setup(x=>x.GetCurrentTime()).Returns(DateTime.UtcNow);
            _sendMailHelperMock=new Mock<ISendMailHelper>();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
