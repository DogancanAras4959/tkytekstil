using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Core;

namespace tkytekstil.CORE.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class, IEntity
    {
        IRepository<T> Repository { get; }
        Task<int> Commit();
        void RollBack();

    }
}
