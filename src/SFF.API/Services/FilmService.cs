using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Services.Interfaces;

namespace SFF.API.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            this._filmRepository = filmRepository;
        }
        public IQueryable<Film> FilmListIncludeCopies()
        {
            return _filmRepository.FilmListIncludeCopies();
        }
        public IQueryable<Film> FilmNoCopiesList()
        {
            return _filmRepository.FilmNoCopiesList();
        }
    }

}