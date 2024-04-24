using Application.InterfaceService;
using Application.Service;
using Application.ViewModel.UserViewModel;
using AutoFixture;
using Backend.Domain.Test;
using Domain.Entities;
using Fare;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Test.ServiceTest
{
    public class UserServiceTest:SetupTest
    {
        private readonly IUserService _userService;
        public UserServiceTest()
        {
            _userService=new UserService(_unitOfWorkMock.Object,_mapper);
        }
        [Fact]
        public async Task Register_ShouldReturnTrue()
        {
            var listUser = new List<User>();
            var registerModel= _fixture.Build<RegisterModel>().With(x=>x.PhoneNumber,new Xeger("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$").Generate).With(x=>x.BirthDay,"2002-09-12").Create();
            _unitOfWorkMock.Setup(um => um.UserRepository.FindAsync(u => u.UserName == registerModel.Username || u.Email == registerModel.Email)).ReturnsAsync(listUser);
            var newAccount = _mapper.Map<User>(registerModel);
            _unitOfWorkMock.Setup(um => um.UserRepository.AddAsync(newAccount)).Verifiable(); 
            _unitOfWorkMock.Setup(um=>um.SaveChangeAsync()).ReturnsAsync(1);
            bool isCreated = await _userService.CreateAccount(registerModel);
            Assert.True(isCreated);
        }
        [Fact]
        public async Task Regiser_ShouldReturnException()
        {
            var listUser=_fixture.Build<User>().CreateMany(1).ToList();
            var registerModel = _fixture.Build<RegisterModel>().With(x => x.PhoneNumber, new Xeger("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$").Generate).With(x => x.BirthDay, "2002-09-12").Create();
            _unitOfWorkMock.Setup(um => um.UserRepository.FindAsync(u => u.UserName == registerModel.Username || u.Email == registerModel.Email)).ReturnsAsync(listUser);
            Func<Task> act = async () =>  await _userService.CreateAccount(registerModel);
            act.Should().ThrowAsync<Exception>();
        }
    }
}
