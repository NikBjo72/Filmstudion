using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Models;

namespace SFF.API.Services.Interfaces
{
    public interface IUserService
    {
        UserAuthenticateModelRequest Authenticate(UserAuthenticateModelRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(UserAuthenticateModelResponce model);
        //void Update(int id, UpdateRequest model);
        void Delete(int id);
    }

}