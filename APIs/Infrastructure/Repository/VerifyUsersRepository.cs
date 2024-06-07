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
    public class VerifyUsersRepository : GenericRepository<VerifyUser>, IVerifyUsersRepository
    {
        private readonly AppDbContext _appDbContext;
        public VerifyUsersRepository(AppDbContext appDbContext, IClaimService claimService, ICurrentTime currentTime) : base(appDbContext, claimService, currentTime)
        {
            _appDbContext = appDbContext;
        }

        public async Task<VerifyUser> FindVerifyUserIdByUserId(Guid userId)
        {
            return await _appDbContext.VerifyUsers.Where(x => x.UserId == userId).SingleOrDefaultAsync();
        }
    }
}
