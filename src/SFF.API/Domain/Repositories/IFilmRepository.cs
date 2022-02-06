using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities.FilmEntity;
using SFF.API.Domain.Services;

namespace SFF.API.Domain.Repositories
{
    public interface IFilmRepository
    {
        Task<Film[]>FilmListIncludeCopiesAsync();
        Task<Film[]>FilmNoCopiesListAsync();
        Task AddAsync(Film film);

    }
}