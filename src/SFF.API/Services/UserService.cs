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
using Microsoft.AspNetCore.Identity;

namespace SFF.API.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IFilmStudioRepository _filmStudioRepository;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserService(
            IUserRepository userRepository,
            IFilmStudioRepository filmStudioRepository,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _filmStudioRepository = filmStudioRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public UserAuthenticateResponceData Authenticate(UserAuthenticateRequestData model)
        {
            var user = _userRepository.QueryableUser().SingleOrDefault(x => x.UserName == model.UserName);

            // Verifierar användarnamn och lösenord
            if (user == null || !BCryptNet.Verify(model.Password, user.Password))
                throw new Exception("Username or password is incorrect");

            // Om autentiseringen var ok
            var response = _mapper.Map<UserAuthenticateResponceData>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            FilmStudio filmstudio = _filmStudioRepository.getFilmStudio(response.FilmStudioId);
            response.FilmStudio = filmstudio;
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

        public async Task<User> Register(UserRegisterRequestData model)
        {
            // kollar om användare redan finns
            if (_userRepository.QueryableUser().Any(x => x.UserName == model.UserName))
                throw new Exception("Username '" + model.UserName + "' is already taken");

            var newUser = _mapper.Map<User>(model);
            newUser.Role = "admin";
            newUser.Token ="";

            // krypterar lösenordet
            newUser.Password = BCryptNet.HashPassword(model.Password);

            // sparar användaren
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            //User user = GetById(newUser.Id);

            // lägger in användaren i en roll
            await _userManager.AddToRoleAsync(newUser, "admin");
            await _unitOfWork.CompleteAsync();
            return newUser;
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

        public async void Delete(string userId)
        {
           _userRepository.Delete(userId);
           await _unitOfWork.CompleteAsync();
        }
    }

}