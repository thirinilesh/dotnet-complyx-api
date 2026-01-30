using ComplyX.Data.DbContexts;
using ComplyX.Data.Entities;
using ComplyX.Repositories.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Repositories.Repositories
{
    public class ProductOwnerRespositories : BaseRespostories<ProductOwner, int>, IProductOwnerRepositories
    {
        public ProductOwnerRespositories(ComplyX.Data.DbContexts.AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
