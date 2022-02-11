using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;

namespace SFF.API.Services.Interfaces
{
    public interface IUserService
    {
        UserAuthenticateResponceData Authenticate(UserAuthenticateRequestData model);
        IQueryable<User> GetAll();
        User GetById(string userId);
        Task<User> Register(UserRegisterRequestData model);
        void Delete(string userId);
    }

}