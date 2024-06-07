using Application.ViewModel.UserModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> FindUserByEmail (string email);
<<<<<<< HEAD
        Task UpdateUserAsync(User user);
=======
        Task<CurrentUserModel> GetCurrentLoginUserAsync(Guid userId);
>>>>>>> 6cdf03bdbe7ae1467a251393b0db2143671688c7
    }
}
