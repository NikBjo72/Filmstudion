using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Persistence.Contexts;

namespace SFF.API.Persistence.Repositories
{
    public class FilmCopyRepository : BaseRepository, IFilmCopyRepository
    {
    
        public FilmCopyRepository(AppDbContext context) : base(context) {}
        public IQueryable<FilmCopy> FilmCopyList()
        {
            IQueryable<FilmCopy> filmCopies = _context.FilmCopies;

            return filmCopies;
        }
        public async Task AddAsync(FilmCopy filmCopy)
	    {
	    	await _context.FilmCopies.AddAsync(filmCopy);
	    }

        public IQueryable<FilmCopy> GetFilmCopiesByFilmId(string filmId)
        {
            IQueryable<FilmCopy> filmCopies = _context.FilmCopies;
            filmCopies = filmCopies.Where(f => f.FilmId == filmId);
            return filmCopies;        
        }
        public void Update(FilmCopy filmCopy)
	    {
	    	_context.FilmCopies.Update(filmCopy);
	    }

    }
}