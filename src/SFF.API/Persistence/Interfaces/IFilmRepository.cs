using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Interfaces
{
    public interface IFilmRepository
    {
        public IQueryable<Film> FilmListIncludeCopies();
        public IQueryable<Film> FilmNoCopiesList();
        public Task AddAsync(Film film);
        public void Update(Film film);

    }
}