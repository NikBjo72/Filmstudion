using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities.FilmEntity;
using SFF.API.Domain.Repositories;
using SFF.API.Domain.Services;

namespace SFF.API.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            this._filmRepository = filmRepository;
        }
        public async Task<Film[]> FilmListIncludeCopiesAsync()
        {
            return await _filmRepository.FilmListIncludeCopiesAsync();
        }
        public async Task<Film[]>FilmNoCopiesListAsync()
        {
            return await _filmRepository.FilmNoCopiesListAsync();
        }
    }

}