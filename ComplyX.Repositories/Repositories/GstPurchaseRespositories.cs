using ComplyX.Data.DbContexts;
using ComplyX.Data.Entities;
using ComplyX.Repositories.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Repositories.Repositories
{
    public class GstPurchaseRespositories : BaseRespostories<GstPurchase, int>, IGstPurchaseRespositories
    {
        public GstPurchaseRespositories(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
