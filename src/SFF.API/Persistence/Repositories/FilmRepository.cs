using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.API.Domain.Entities.FilmEntity;
using SFF.API.Domain.Repositories;
using SFF.API.Persistence.Contexts;

namespace SFF.API.Persistence.Repositories
{
    public class FilmRepository : BaseRepository, IFilmRepository
    {
    
        public FilmRepository(AppDbContext context) : base(context) {}

        public async Task<Film[]> FilmListIncludeCopiesAsync()
        {
            IQueryable<Film> films;

            films = _context.Films
            .Include(c => c.FilmCopies);

            return await films.ToArrayAsync();
        }
        public async Task<Film[]> FilmNoCopiesListAsync()
        {
            IQueryable<Film> films = _context.Films;

            return await films.ToArrayAsync();
        }
        public async Task AddAsync(Film film)
	    {
	    	await _context.Films.AddAsync(film);
	    }

    }
}