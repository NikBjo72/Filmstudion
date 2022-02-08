using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Persistence.Repositories;
using SFF.API.Services.Interfaces;
using SFF.API.Transfer;

namespace SFF.API.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IFilmCopyRepository _filmCopyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FilmService(IFilmRepository filmRepository, IMapper mapper, IUnitOfWork unitOfWork, IFilmCopyRepository filmCopyRepository)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _filmCopyRepository = filmCopyRepository;
        }
        public IQueryable<Film> FilmListIncludeCopies()
        {
            return _filmRepository.FilmListIncludeCopies();
        }
        public IQueryable<Film> FilmNoCopiesList()
        {
            return _filmRepository.FilmNoCopiesList();
        }

        public async Task<Film> CreateNewFilm(CreateFilmRequestData model)
        {
            // kollar om användare redan finns
            if (_filmRepository.FilmNoCopiesList().Any(x => x.Name == model.Name))
                throw new Exception("Filmen med namnet '" + model.Name + "' finns redan");

            Film newFilm = _mapper.Map<Film>(model);
            
            // Sparar filmen
            await _filmRepository.AddAsync(newFilm);
            await _unitOfWork.CompleteAsync();

            // Gör kopior av filmen
            for (int f = 0; f < model.NumberOfCopies; f++)
            {
                FilmCopy filmCopy = new FilmCopy {
                    FilmId = newFilm.FilmId,
                    RentedOut = false,
                    FilmStudioId = null,
                    Rented = new DateTime(),
                    };

                await _filmCopyRepository.AddAsync(filmCopy);
            }
            await _unitOfWork.CompleteAsync();

            return newFilm;
        }
    }

}