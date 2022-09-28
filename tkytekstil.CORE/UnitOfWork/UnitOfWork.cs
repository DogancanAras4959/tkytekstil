using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL;
using tkytekstil.DAL.Core;

namespace tkytekstil.CORE.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class, IEntity
    {
        private readonly tkytekstildbcontext _dbContext;

        public UnitOfWork(tkytekstildbcontext dbContext)
        {
            _dbContext = dbContext;
            Repository = new Repository<T>(dbContext);
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void RollBack()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
        public IRepository<T> Repository { get; }
    }
}
