using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Interfaces
{
    public interface IFilmCopyRepository
    {
        public IQueryable<FilmCopy> FilmCopyList();
        public Task AddAsync(FilmCopy filmCopy);
        public IQueryable<FilmCopy> GetFilmCopiesByFilmId(string filmId);
        public void Update(FilmCopy filmCopy);

    }
}