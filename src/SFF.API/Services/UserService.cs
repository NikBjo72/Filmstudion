using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SFF.API.Domain.Authorization;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;
using SFF.API.Persistence.Contexts;
using BCryptNet = BCrypt.Net.BCrypt;
using SFF.API.Persistence.Interfaces;
using SFF.API.Services.Interfaces;

namespace SFF.API.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public UserAuthenticateResponceData Authenticate(UserAuthenticateRequestData model)
        {
            var user = _userRepository.QueryableUser().SingleOrDefault(x => x.UserName == model.UserName);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.Password))
                throw new Exception("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<UserAuthenticateResponceData>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.QueryableUser();
        }

        public User GetById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public void Register(UserRegisterRequestData model)
        {
            // validate
            if (_userRepository.QueryableUser().Any(x => x.UserName == model.UserName))
                throw new Exception("Username '" + model.UserName + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.Password = BCryptNet.HashPassword(model.Password);

            // save user
            _userRepository.AddAsync(user);
        }

        // public void Update(int id, UpdateRequest model)
        // {
        //     var user = getUser(id);

        //     // validate
        //     if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
        //         throw new AppException("Username '" + model.Username + "' is already taken");

        //     // hash password if it was entered
        //     if (!string.IsNullOrEmpty(model.Password))
        //         user.PasswordHash = BCryptNet.HashPassword(model.Password);

        //     // copy model to user and save
        //     _mapper.Map(model, user);
        //     _context.Users.Update(user);
        //     _context.SaveChanges();
        // }

        public void Delete(string userId)
        {
           _userRepository.Delete(userId);
        }
    }

}