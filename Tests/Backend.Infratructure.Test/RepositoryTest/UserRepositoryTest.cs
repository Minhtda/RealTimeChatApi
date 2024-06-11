using Application.InterfaceRepository;
using Application.ViewModel.UserModel;
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
    public class UserRepositoryTest : SetupTest
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
            await _dbContext.AddAsync(mockUserData);
            await _dbContext.SaveChangesAsync();
            var user = await _userRepository.FindUserByEmail(mockUserData.Email);
            Assert.True(user != null);
            user.Id.Should().Be(mockUserData.Id);
        }
        [Fact]
        public async Task GetCurrentUser_ShouldReturnCorrectData()
        {
            //Arrange
            var mockUser=_fixture.Build<User>().Create();
            var getCurrentUserMock=_fixture.Build<CurrentUserModel>()
                .With(x=>x.Phonenumber,mockUser.PhoneNumber)
                .With(x=>x.Birthday,DateOnly.FromDateTime(mockUser.BirthDay.Value))
                .With(x=>x.Email,mockUser.Email)
                .With(x=>x.Fullname,mockUser.FirstName+" "+mockUser.LastName)
                .Create();
                
            //Act
           await _dbContext.AddAsync(mockUser);
           await _dbContext.SaveChangesAsync();
           var result= await _userRepository.GetCurrentLoginUserAsync(mockUser.Id);
           //Assert
           result.Email.Should().Be(getCurrentUserMock.Email); 
        }
    }
}
