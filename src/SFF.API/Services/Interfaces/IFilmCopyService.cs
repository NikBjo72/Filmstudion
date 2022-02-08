using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Services.Interfaces
{
    public interface IFilmCopyService
    {
        public IQueryable<FilmCopy> FilmCopyList();
        public void AddAsync(FilmCopy filmCopy);
        public IQueryable<FilmCopy> GetFilmCopiesByFilmId(string filmId);
    }

}