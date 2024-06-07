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
        private readonly IProductRepository _productRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IVerifyUsersRepository _verifyUsersRepository;
        public UnitOfWork(IUserRepository userRepository, AppDbContext dbContext, 
            IPostRepository postRepository, IProductRepository productRepository, IWalletRepository walletRepository, IVerifyUsersRepository verifyUsersRepository)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _postRepository = postRepository;
            _productRepository = productRepository;
            _walletRepository = walletRepository;
            _verifyUsersRepository = verifyUsersRepository;
        }

        public IUserRepository UserRepository =>_userRepository;


        public IPostRepository PostRepository => _postRepository;

        public IProductRepository ProductRepository => _productRepository;

        public IWalletRepository WalletRepository => _walletRepository;
        public IVerifyUsersRepository VerifyUsersRepository => _verifyUsersRepository;

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
