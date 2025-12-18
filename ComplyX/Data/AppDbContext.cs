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
        public virtual DbSet<CompanyEPFO> CompanyEPFO { get; set; }
        public virtual DbSet<EmployeeEPFO> EmployeeEPFO { get; set; }

        public virtual DbSet<EPFOECRFile> EPFOECRFile { get; set; }

        public virtual DbSet<EPFOPeriod> EPFOPeriod { get; set; }
        public virtual DbSet<LicenseKeyMaster> LicenseKeyMaster { get; set; }
        public virtual DbSet<LicenseActivation> LicenseActivation { get; set; }

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

            modelBuilder.Entity<CompanyEPFO>(entity =>
            {
                entity.HasKey(e => e.CompanyEPFOId);

                entity.HasOne(d => d.Companies).WithMany(p => p.CompanyEPFO)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CompanyEPFO_Company");
            });

            modelBuilder.Entity<EmployeeEPFO>(entity =>
            {
                entity.HasKey(e => e.EmployeeEPFOId);

                entity.HasOne(d => d.Employees).WithMany(p => p.EmployeeEPFO)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeEPFO_Employee");
            });

            modelBuilder.Entity<EPFOECRFile>(entity =>
            {
                entity.HasKey(e => e.ECRFileId);

                entity.HasOne(d => d.Companies).WithMany(p => p.EPFOECRFile)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_EPFOECRFile_Company");

                entity.HasOne(d => d.Subcontractorss).WithMany(p => p.EPFOECRFile)
                    .HasForeignKey(d => d.SubcontractorId)
                    .HasConstraintName("FK_EPFOECRFile_Subcontractor");
            });

            modelBuilder.Entity<EPFOPeriod>(entity =>
            {
                entity.HasKey(e => e.EPFOPeriodId);

                entity.HasOne(d => d.Companies).WithMany(p => p.EPFOPeriods)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_EPFOECRFile_Company");

                entity.HasOne(d => d.Subcontractorss).WithMany(p => p.EPFOPeriods)
                    .HasForeignKey(d => d.SubcontractorId)
                    .HasConstraintName("FK_EPFOECRFile_Subcontractor");

                entity.HasOne(d => d.CreatedByUser).WithMany(p => p.CreatedEPFOPeriods)
                   .HasForeignKey(d => d.CreatedByUserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<LicenseKeyMaster>(entity =>
            {
                entity.HasKey(e => e.LicenseId);

                entity.HasOne(d => d.ProductOwners).WithMany(p => p.LicenseKeyMaster)
                    .HasForeignKey(d => d.ProductOwnerId)
                    .HasConstraintName("FK_LicenseKeyMaster_ProductOwners");
            });

            modelBuilder.Entity<LicenseActivation>(entity =>
            {
                entity.HasKey(e => e.ActivationId);

                entity.HasOne(d => d.LicenseKeyMaster).WithMany(p => p.LicenseActivation)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK_LicenseActivation_LicenseKeyMaster");
            });


            modelBuilder.Entity<LicenseAuditLogs>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.HasOne(d => d.LicenseKeyMaster).WithMany(p => p.LicenseAuditLogs)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK_LicenseAuditLogs_LicenseKeyMaster");

                entity.HasOne(d => d.LicenseActivation).WithMany(p => p.LicenseAuditLogs)
                    .HasForeignKey(d => d.ActivationId)
                    .HasConstraintName("FK_LicenseAuditLogs_LicenseActivation");
            });
        }
      

    }
}
