using Application;
using Application.InterfaceRepository;
using Application.InterfaceService;
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
        
        private readonly IPostRepository _postRepository;
        public UnitOfWork(IUserRepository userRepository, AppDbContext dbContext, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _postRepository = postRepository;
        }

        public IUserRepository UserRepository =>_userRepository;


        public IPostRepository PostRepository => _postRepository;

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
