using Application;
using Application.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        public UnitOfWork(IUserRepository userRepository, AppDbContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository =>_userRepository;

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
