using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Persistence.Contexts;

namespace SFF.API.Persistence.Repositories
{
    public class FilmStudioRepository : BaseRepository, IFilmStudioRepository
    {
    
        public FilmStudioRepository(AppDbContext context) : base(context) {}

        public IQueryable<FilmStudio> QueryableFilmStudioIncludeFilmCopies()
        {
            IQueryable<FilmStudio> filmStudios;

            filmStudios = _context.FilmStudios
            .Include(c => c.FilmCopies);

            return filmStudios;
        }
        public IQueryable<FilmStudio> QueryableFilmStudioNoFilmCopies()
        {
            IQueryable<FilmStudio> filmStudios = _context.FilmStudios;

            return filmStudios;
        }
        public async Task AddAsync(FilmStudio filmStudio)
	    {
	    	await _context.FilmStudios.AddAsync(filmStudio);
	    }

        public FilmStudio GetByIdIncludeFilmCopies(string filmStudioId)
        {
            return getFilmStudio(filmStudioId);
        }
        public FilmStudio getFilmStudio(string filmStudioId)
        {
            var filmstudio = _context.FilmStudios.Find(filmStudioId);
            if (filmstudio == null) throw new KeyNotFoundException("Filmstudio not found");
            return filmstudio;
        }
    }
}