using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ComplyX.Shared.Data;

using ComplyX_Businesss.Models;


namespace ComplyX.Shared.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<ProductOwners> ProductOwners { get; set; }
        public virtual DbSet<RegisterUser> RegisterUser { get; set; }
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
        public virtual DbSet<LicenseAuditLogs> LicenseAuditLogs { get; set; }
        public virtual DbSet<MachineBinding> MachineBinding { get; set; }

        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<SubscriptionInvoices> SubscriptionInvoices { get; set; }
        public virtual DbSet<PaymentTransactions> PaymentTransactions { get; set; }
        public virtual DbSet<CustomerPayments> CustomerPayments { get; set; }
        public virtual DbSet<Gratuity_Policy> Gratuity_Policy { get; set; }
        public virtual DbSet<Gratuity_Transactions> Gratuity_Transactions { get; set; }
        public virtual DbSet<FnF_Calculations> FnF_Calculations { get; set; }
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

            modelBuilder.Entity<MachineBinding>(entity =>
            {
                entity.HasKey(e => e.MachineBindingId);

                entity.HasOne(d => d.LicenseActivation).WithMany(p => p.MachineBinding)
                     .HasForeignKey(d => d.LicenseActivationId)
                   .HasPrincipalKey(u => u.ActivationId)
                   .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MachineBindingId_LicenseActivation");
            });

            modelBuilder.Entity<SubscriptionInvoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionInvoices)
                     .HasForeignKey(d => d.CompanyID)                 
                    .HasConstraintName("FK_SubscriptionInvoices_Company");
                entity.HasOne(d => d.CustomerPayments).WithMany(p => p.SubscriptionInvoices)
                   .HasForeignKey(d => d.PaymentID)
                  .HasConstraintName("FK_SubscriptionInvoices_CustomerPayments");
            });

            modelBuilder.Entity<PaymentTransactions>(entity =>
            {
                entity.HasKey(e => e.TransactionID);

                entity.HasOne(d => d.CustomerPayments).WithMany(p => p.PaymentTransactions)
                     .HasForeignKey(d => d.PaymentID)
                    .HasConstraintName("FK_PaymentTransactions_CustomerPayments");
                 
            });
            modelBuilder.Entity<CustomerPayments>(entity =>
            {
                entity.HasKey(e => e.PaymentID);

                entity.HasOne(d => d.Company).WithMany(p => p.CustomerPayments)
                     .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_CustomerPayments_Company");

                entity.HasOne(d => d.Plans).WithMany(p => p.CustomerPayments)
                    .HasForeignKey(d => d.PlanID)
                   .HasConstraintName("FK_CustomerPayments_Plans");

            });
            modelBuilder.Entity<Gratuity_Policy>(entity =>
            {
                entity.HasKey(e => e.PolicyID);

                entity.HasOne(d => d.Company).WithMany(p => p.Gratuity_Policy)
                     .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Gratuity_Policy_Company");
            });
            modelBuilder.Entity<Gratuity_Transactions>(entity =>
            {
                entity.HasKey(e => e.GratuityID);

                entity.HasOne(d => d.Employees).WithMany(p => p.Gratuity_Transactions)
                     .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Gratuity_Transactions_Employees");
            });
        }
      

    }
}
