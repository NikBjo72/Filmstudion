using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Interfaces
{
    public interface IFilmRepository
    {
        IQueryable<Film> FilmListIncludeCopies();
        IQueryable<Film> FilmNoCopiesList();
        Task AddAsync(Film film);

    }
}