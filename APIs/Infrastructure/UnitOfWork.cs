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
        private readonly IPostRepository _postRepository;
        public UnitOfWork(IUserRepository userRepository, AppDbContext dbContext, ICacheRepository cacheRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _cacheRepository = cacheRepository;
            _postRepository = postRepository;
        }

        public IUserRepository UserRepository =>_userRepository;

        public ICacheRepository CacheRepository =>_cacheRepository;

        public IPostRepository PostRepository => _postRepository;

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
