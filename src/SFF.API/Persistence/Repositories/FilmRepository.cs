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
        public FilmRepository(AppDbContext context) : base(context){}

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
	    }

        public void Update(Film film)
	    {
	    	_context.Films.Update(film);
	    }

    }
}