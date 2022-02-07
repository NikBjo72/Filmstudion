using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Services.Interfaces
{
    public interface IFilmService
    {
        IQueryable<Film> FilmListIncludeCopies();
        IQueryable<Film> FilmNoCopiesList();
    }

}