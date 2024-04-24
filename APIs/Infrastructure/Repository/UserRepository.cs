using Application.InterfaceRepository;
using Application.InterfaceService;
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
    }
}
