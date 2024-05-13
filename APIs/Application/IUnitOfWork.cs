using Application.InterfaceRepository;
using Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get;}
        public ICacheRepository CacheRepository { get;}
        public IPostRepository PostRepository { get;}
        public Task<int> SaveChangeAsync();
    }
}
