using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
       public Task CompleteAsync();
    }
}