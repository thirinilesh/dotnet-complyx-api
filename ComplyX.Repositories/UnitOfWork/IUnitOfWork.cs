using System;
using ComplyX.Repositories.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyX.Repositories.Repositories.Abstractions;

namespace ComplyX.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IProductOwnerRepositories ProductOwnerRepositories { get; }
    }
}
