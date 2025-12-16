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
        public virtual DbSet<Subcontractors> Subcontractors { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<PayrollData> PayrollData { get; set; }
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

            modelBuilder.Entity<Subcontractors>(entity =>
            {
                entity.HasKey(e => e.SubcontractorID);

                entity.HasOne(d => d.Companies).WithMany(p => p.Subcontractorss)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Subcontractors_Company");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);

                entity.HasOne(d => d.Companies).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Employee_Company");

                entity.HasOne(d => d.Subcontractor).WithMany(p => p.Employees)
                   .HasForeignKey(d => d.SubcontractorID)
                   .HasConstraintName("FK_Employee_Subcontractor");
            });

            modelBuilder.Entity<PayrollData>(entity =>
            {
                entity.HasKey(e => e.PayrollID);

                entity.HasOne(d => d.Employees).WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Payroll_Employee");
            });
        }
      

    }
}
