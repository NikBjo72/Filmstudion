using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Interfaces;
using SFF.API.Persistence.Contexts;

namespace SFF.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
    
        public UserRepository(AppDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) {}

        public IQueryable<User> QueryableUser()
        {
             IQueryable<User> users = _context.Users;
             return users;
        }

        public async Task AddAsync(User user)
	    {
	    	await _context.Users.AddAsync(user);
	    }

        public async Task Delete(string userId)
	    {
            User user = getUser(userId);
	    	_context.Users.Remove(user);
            await _unitOfWork.CompleteAsync();
	    }

        public User GetById(string userId)
        {
            return getUser(userId);
        }

        private User getUser(string userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}