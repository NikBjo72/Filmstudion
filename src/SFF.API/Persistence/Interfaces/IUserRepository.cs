using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Interfaces
{
    public interface IUserRepository
    {
       public IQueryable<User> QueryableUser();
       Task AddAsync(User user);
       Task Delete(string userId);
       User GetById(string UserId);
    }
}