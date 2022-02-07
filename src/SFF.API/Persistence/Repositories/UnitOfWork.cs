using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;
using SFF.API.Persistence.Contexts;

namespace SFF.API.Persistence.Interfaces
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