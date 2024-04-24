using Application;
using Application.InterfaceRepository;
using Infrastructure.Cache;
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
        private readonly ICacheRepository _cacheRepository;
        public UnitOfWork(IUserRepository userRepository, AppDbContext dbContext, ICacheRepository cacheRepository)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _cacheRepository = cacheRepository;
        }

        public IUserRepository UserRepository =>_userRepository;

        public ICacheRepository CacheRepository =>_cacheRepository;

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
