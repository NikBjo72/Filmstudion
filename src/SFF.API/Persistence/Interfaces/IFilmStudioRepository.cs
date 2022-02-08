using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Interfaces
{
    public interface IFilmStudioRepository
    {
       public IQueryable<FilmStudio> QueryableFilmStudioIncludeFilmCopies();
       public IQueryable<FilmStudio> QueryableFilmStudioNoFilmCopies();
       public Task AddAsync(FilmStudio filmStudio);
       public FilmStudio GetByIdIncludeFilmCopies(string filmStudioId);
    }
}