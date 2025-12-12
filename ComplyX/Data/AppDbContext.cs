using System.Collections.Generic;
using ComplyX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace ComplyX.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<ProductOwners> ProductOwners { get; set; }
        public DbSet<RegisterUser> RegisterUser { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<SubscriptionPlans> SubscriptionPlans { get; set; }
        public virtual DbSet<ProductOwnerSubscriptions> ProductOwnerSubscriptions { get; set; }
        public virtual DbSet<ProductOwnerSubscriptions> ProductOwnerSubscriptionsDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RegisterUser>().HasKey(e =>e.UserName);


            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey( e => e.CompanyID);

                entity.HasOne(d => d.ProductOwners).WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ProductOwnerId)
                    .HasConstraintName("FK_Company_ProductOwner");
            });

            modelBuilder.Entity<ProductOwnerSubscriptions>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.HasOne(d => d.SubscriptionPlans).WithMany(p => p.ProductOwnerSubscriptionss)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK_ProductOwnerSubscriptions_SubscriptionPlans");

                entity.HasOne(d => d.ProductOwners).WithMany(p => p.ProductOwnerSubscriptionss)
                  .HasForeignKey(d => d.ProductOwnerId)
                  .HasConstraintName("FK_ProductOwnerSubscriptions_ProductOwner");
            });
        }
      

    }
}
