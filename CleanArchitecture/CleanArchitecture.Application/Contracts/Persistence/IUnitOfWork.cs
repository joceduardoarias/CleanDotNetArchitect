using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{    
    IAsyncRepository<TEntity> Respository<TEntity>() where TEntity : BaseDomainModel;
    Task<int> Complete();
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
