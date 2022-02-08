using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Transfer;

namespace SFF.API.Services.Interfaces
{
    public interface IFilmStudioService
    {
        public IQueryable<FilmStudio> QueryableFilmStudioIncludeFilmCopies();
        public IQueryable<FilmStudio> QueryableFilmStudioNoFilmCopies();
        public Task<RegisterFilmStudioResponceData> RegisterFilmStudio(RegisterFilmStudioRequestData model);
        public FilmStudio GetByIdIncludeFilmCopies(string filmStudioId);
    }

}