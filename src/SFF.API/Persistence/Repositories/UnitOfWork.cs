using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Contexts;
using SFF.API.Persistence.Interfaces;

namespace SFF.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;     
        }

       public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}