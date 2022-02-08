using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Persistence.Contexts;

namespace SFF.API.Persistence.Repositories
{
    public class FilmRepository : BaseRepository, IFilmRepository
    {
        //private readonly UnitOfWork _unitOfWork;
        public FilmRepository(AppDbContext context/*, UnitOfWork unitOfWork*/) : base(context)
        {
           //_unitOfWork = unitOfWork;
        }

        // public async Task<Film[]> FilmListIncludeCopiesAsync()
        // {
        //     IQueryable<Film> films;

        //     films = _context.Films
        //     .Include(c => c.FilmCopies);

        //     return await films.ToArrayAsync();
        // }

        public IQueryable<Film> FilmListIncludeCopies()
        {
            IQueryable<Film> films;

            films = _context.Films
            .Include(c => c.FilmCopies);

            return films;
        }
        public IQueryable<Film> FilmNoCopiesList()
        {
            IQueryable<Film> films = _context.Films;

            return films;
        }
        public async Task AddAsync(Film film)
	    {
	    	await _context.Films.AddAsync(film);
            //await _unitOfWork.CompleteAsync();
	    }

    }
}