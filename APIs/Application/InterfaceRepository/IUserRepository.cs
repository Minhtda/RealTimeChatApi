﻿using Domain.Entities;
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
    }
}