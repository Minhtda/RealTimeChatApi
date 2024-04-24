using Application.InterfaceRepository;
using AutoFixture;
using Backend.Domain.Test;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infratructure.Test.RepositoryTest
{
    public class UserRepositoryTest:SetupTest
    {
        private readonly IUserRepository _userRepository;
        public UserRepositoryTest()
        {
            _userRepository = new UserRepository(
                _dbContext,
                _claimServiceMock.Object,
                _currentTimeMock.Object
                );
        }
        [Fact]
        public async Task CheckEmailExisted_ShouldReturnTrue()
        {
            var mockUserData = _fixture.Build<User>().Create();
            await _dbContext.AddAsync( mockUserData );
            await _dbContext.SaveChangesAsync();
          var user =await _userRepository.FindUserByEmail(mockUserData.Email);
            Assert.True( user != null );    
            user.Id.Should().Be( mockUserData.Id ); 
        }
    }
}
