using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Tests.Service
{
    public class AppDbContext : DbContext 
    { 
        public AppDbContext() { } 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
        public DbSet<Companies> Companiess { get; set; }
        public DbSet<PartnerApiAuthConfig> PartnerApiAuthConfig { get; set; }
    }
}
