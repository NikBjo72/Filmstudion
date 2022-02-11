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
    public class FilmStudioService : IFilmStudioService
    {
        private readonly IFilmStudioRepository _filmStudioRepository;
        private IUserRepository _userRepository;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public FilmStudioService(
            IUserRepository userRepository,
            IFilmStudioRepository filmStudioRepository,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _filmStudioRepository = filmStudioRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public IList<FilmStudioNoCityResponceData> ListFilmStudioIncludeFilmCopiesNoCity()
        {
            var filmstudios = _filmStudioRepository.QueryableFilmStudioIncludeFilmCopies();
            var list = filmstudios.ToList();
            IList<FilmStudioNoCityResponceData> newList = new List<FilmStudioNoCityResponceData>();
            for (var i = 0; i < list.Count; i++)
            {
                var test = _mapper.Map<FilmStudioNoCityResponceData>(list[i]);
                newList.Add(test);
            }
            return newList;
            //return _filmStudioRepository.QueryableFilmStudioIncludeFilmCopies();
        }
        public IQueryable<FilmStudio> QueryableFilmStudioIncludeFilmCopies()
        {
            return _filmStudioRepository.QueryableFilmStudioIncludeFilmCopies();
        }
        public IQueryable<FilmStudio> QueryableFilmStudioNoFilmCopies()
        {
            return _filmStudioRepository.QueryableFilmStudioNoFilmCopies();
        }
        public async Task<RegisterFilmStudioResponceData> RegisterFilmStudio(RegisterFilmStudioRequestData model)
        {
            // kollar om användare redan finns
            if (_userRepository.QueryableUser().Any(x => x.UserName == model.UserName))
                throw new Exception("Username '" + model.UserName + "' is already taken");

            if (_filmStudioRepository.QueryableFilmStudioNoFilmCopies().Any(x => x.Name == model.Name))
            throw new Exception("Filmstudio name '" + model.Name + "' is already taken");

            var newFilmStudioUser = _mapper.Map<User>(model);
            newFilmStudioUser.Role = "filmstudio";
            //newFilmStudioUser.Token ="";

            // Krypterar lösenordet
            newFilmStudioUser.Password = BCryptNet.HashPassword(model.Password);

            // Skapar en ny fimstudio
            var newFilmStudio = _mapper.Map<FilmStudio>(model);
            await _filmStudioRepository.AddAsync(newFilmStudio);
            await _unitOfWork.CompleteAsync();

            // Kopplar User och filmstudio
            newFilmStudioUser.FilmStudioId = newFilmStudio.FilmStudioId;

            // sparar användaren
            await _userRepository.AddAsync(newFilmStudioUser);
            await _unitOfWork.CompleteAsync();

            // lägger in användaren i en roll
            await _userManager.AddToRoleAsync(newFilmStudioUser, "filmstudio");
            await _unitOfWork.CompleteAsync();

            FilmStudio filmStudio = GetByIdIncludeFilmCopies(newFilmStudio.FilmStudioId);
            RegisterFilmStudioResponceData responceData = _mapper.Map<RegisterFilmStudioResponceData>(filmStudio);

            return responceData;
        }
        public FilmStudio GetByIdIncludeFilmCopies(string filmStudioId)
        {
            return _filmStudioRepository.GetByIdIncludeFilmCopies(filmStudioId);
        }

    }

}