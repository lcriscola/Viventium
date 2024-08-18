using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Viventium.Models.DB;

namespace Viventium.Repositores.Infrastructure
{
    public interface IGenericRepository
    {
        void Add<T>(T entity) where T : notnull;
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransaction(IDbContextTransaction transaction);
        DbSet<T> Set<T>() where T : class;
        Task SaveChangesAsync();

        Task ExecuteDeleteAsync<T>() where T : class;
    }
}
