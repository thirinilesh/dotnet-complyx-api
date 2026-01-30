using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Repositories.UnitOfWork
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
