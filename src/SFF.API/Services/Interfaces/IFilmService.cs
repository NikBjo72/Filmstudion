using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;

namespace SFF.API.Services.Interfaces
{
    public interface IFilmService
    {
        public IQueryable<Film> FilmListIncludeCopies();
        public IQueryable<Film> FilmNoCopiesList();
        public Task<Film> CreateNewFilm(CreateFilmRequestData model);
    }

}