using SFF.API.Persistence.Contexts;
using SFF.API.Persistence.Interfaces;

namespace SFF.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;
        protected readonly UnitOfWork _unitOfWork;

        public BaseRepository(
            AppDbContext context,
            UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
    }
}