using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.ViewModel.UserModel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IClaimService _claimService;
        public UserRepository(AppDbContext appDbContext, IClaimService claimService, ICurrentTime currentTime) : base(appDbContext, claimService, currentTime)
        {
            _dbContext = appDbContext;
            _claimService = claimService;
        }

        public async Task<User> FindUserByEmail(string email)
        {
          User user= await _dbContext.Users.Include(x=>x.Role).FirstOrDefaultAsync(x=>x.Email==email);
          return user;
        }
        public async Task UpdateUserAsync(User user)
        {
            _dbSet.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CurrentUserModel> GetCurrentLoginUserAsync(Guid userId)
        {
            var currentLoginUser=await GetByIdAsync(userId);
            CurrentUserModel currentUserModel = new CurrentUserModel()
            {
                Username=currentLoginUser.UserName,
                Email=currentLoginUser.Email,
                Birthday=DateOnly.FromDateTime(currentLoginUser.BirthDay),
                Fullname=currentLoginUser.FirstName+" "+currentLoginUser.LastName,
                Phonenumber=currentLoginUser.PhoneNumber
            };
            return currentUserModel;
        }
    }
}
