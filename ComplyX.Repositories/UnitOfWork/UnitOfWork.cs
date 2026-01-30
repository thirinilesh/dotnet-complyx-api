using ComplyX.Repositories.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using ComplyX.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyX.Repositories.Repositories;
using ComplyX.Data.DbContexts;

namespace ComplyX.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext   dbcontext;
        private readonly AppDbContext appDbContext;
        public IProductOwnerRepositories ProductOwnerRepositories { get; }
        public UnitOfWork (DbContext dbcontext, IProductOwnerRepositories productOwnerRespositories, AppDbContext appDbContext)
        {
            this.dbcontext = dbcontext;
            ProductOwnerRepositories = productOwnerRespositories;
            this.appDbContext = appDbContext;

        }
        public async Task CommitAsync() 
        {
            try 
            { 
                await appDbContext.SaveChangesAsync(); 
            } catch (Exception ex) 
            {
                throw; 
            }
        }
        void IDisposable.Dispose() 
        { 
            if (appDbContext != null) 
            {
                appDbContext.Dispose(); 
            } 
        }
     
    }
}
