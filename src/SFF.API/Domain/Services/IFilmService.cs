using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities.FilmEntity;

namespace SFF.API.Domain.Services
{
    public interface IFilmService
    {
        Task<Film[]> FilmListIncludeCopiesAsync();
        Task<Film[]>FilmNoCopiesListAsync();
    }

}