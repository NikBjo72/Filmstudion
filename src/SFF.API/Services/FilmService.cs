using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SFF.API.Domain.Entities;
using SFF.API.Domain.Entities.Interfaces;
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
        
        public Film UpdateFilm(string filmId, PatchFilmRequestData model)
        {
            // Lägger till id:t i modellen
            model.FilmId = filmId;
            
            // kollar om användare redan finns
            if (!_filmRepository.FilmNoCopiesList().Any(x => x.FilmId == filmId))
                throw new Exception("Film med detta id finns inte");

            // gör om model till Film och uppdaterar
            Film updatedFilm = _mapper.Map<Film>(model);
            _filmRepository.Update(updatedFilm);
            _unitOfWork.CompleteAsync();

            Film oldFilm = _filmRepository
            .FilmListIncludeCopies()
            .Where(f => f.FilmId == filmId)
            .FirstOrDefault();

            var filmCopies = _filmCopyRepository
                .FilmCopyList()
                .Where(f => f.FilmId == filmId);

            // Skapar eller tar bort kopior av filmen om antalet ändrats
            if (filmCopies.Count() != model.NumberOfCopies)         {

                if (filmCopies.Count() < model.NumberOfCopies)
                {
                    for (var i = filmCopies.Count(); i <= model.NumberOfCopies; i++)
                    {
                        FilmCopy filmCopy = new FilmCopy {
                        FilmId = model.FilmId,
                        RentedOut = false,
                        FilmStudioId = null,
                        Rented = new DateTime(),
                        };
                        _filmCopyRepository.AddAsync(filmCopy);
                    }
                    _unitOfWork.CompleteAsync();
                }
                if (filmCopies.Count() > model.NumberOfCopies)
                {
                    for (var i = filmCopies.Count(); i >= model.NumberOfCopies; i--)
                    {
                        var deleteFilmCopy = filmCopies.FirstOrDefault();
                        _filmCopyRepository.Delete(deleteFilmCopy);
                    }
                }
            }
            _unitOfWork.CompleteAsync();
            return oldFilm;

        }

        public void RentFilm(string filmId, string studioId)
        {
            bool areadyRented = _filmCopyRepository
                .FilmCopyList()
                .Where(f => f.FilmId == filmId)
                .Any(f => f.FilmStudioId == studioId);
            if (areadyRented) throw new Exception("Du hyr redan denna filmen");

            FilmCopy filmCopy = _filmCopyRepository
                .FilmCopyList()
                .Where(f => f.FilmId == filmId)
                .FirstOrDefault();

            filmCopy.FilmStudioId = studioId;
            filmCopy.RentedOut = true;
            filmCopy.Rented = DateTime.Today;

            _filmCopyRepository.Update(filmCopy);
            _unitOfWork.CompleteAsync();    
        }

        public void ReturnFilm(string filmId, string studioId)
        {
            FilmCopy filmCopy = _filmCopyRepository
                .FilmCopyList()
                .Where(f => f.FilmId == filmId)
                .Where(f => f.FilmStudioId == studioId)
                .FirstOrDefault();

            filmCopy.FilmStudioId = null;
            filmCopy.RentedOut = false;
            filmCopy.Rented = new DateTime();

            _filmCopyRepository.Update(filmCopy);
            _unitOfWork.CompleteAsync();    
        }

        public List<FilmCopy> GetAllRentedFilms (string filmStudioId)
        {
            // *** Skapar två nya listor ***
            //IList<string> filmId = new List<string>();
            //List<Film> result = new List<Film>();

            // *** Hämtar alla filme samt alla kopior på filmer som har filstudions id ***
            //var allFilms = _filmRepository.FilmListIncludeCopies().ToList();
            var filmCopies = _filmCopyRepository.FilmCopyList()
                .Where(f => f.FilmStudioId == filmStudioId).ToList();

            if (filmCopies.Count() == 0) throw new Exception("Du har inga hyrda filmer");
            
            // *** Lägger in alla id:n i en lista på filmer som filstudion har hyrt ***
            // foreach (var film in filmCopies)
            // {
            //     filmId.Add(film.FilmId);
            // }
            // *** Använder alla id:n i listan för att göra en lista på alla filmer som filstudion hyrt ***
            // foreach (var id in filmId)
            // {
            //     Film film = allFilms.Where(f => f.FilmId == id).FirstOrDefault();
            //     result.Add(film);
            // }
            return filmCopies;

        }

        public void Delete(FilmCopy filmCopy)
        {
           _filmCopyRepository.Delete(filmCopy);
           _unitOfWork.CompleteAsync();
        }
    }

}