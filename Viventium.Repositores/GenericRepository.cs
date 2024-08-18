using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Viventium.Repositores.Infrastructure;

namespace Viventium.Repositores
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ViventiumDataContext _db;

        public GenericRepository(ViventiumDataContext db)
        {
            _db = db;
        }

        public void Add<T>(T entity) where T : notnull
        {
            _db.Add(entity);
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _db.Set<T>();
        }

        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }


        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _db.Database.BeginTransactionAsync();
        }
        public Task CommitTransaction(IDbContextTransaction transaction)
        {
            return transaction.CommitAsync();
        }

        public Task ExecuteDeleteAsync<T>() where T : class
        {
            return _db.Set<T>().ExecuteDeleteAsync();
        }
    }
}
