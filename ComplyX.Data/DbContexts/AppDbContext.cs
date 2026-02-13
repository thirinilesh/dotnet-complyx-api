using System;
using System.Collections.Generic;
using ComplyX.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ComplyX.Data.Entities;

namespace ComplyX.Data.DbContexts;

public partial class AppDbContext :IdentityDbContext<ApplicationUsers>
{
  

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ApplicationUsers> ApplicationUsers { get; set; }
    public virtual DbSet<AccountOwner> AccountOwners { get; set; }

    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyEpfo> CompanyEpfos { get; set; }

    public virtual DbSet<CompanyPartyRole> CompanyPartyRoles { get; set; }

    public virtual DbSet<CompanySubcontractor> CompanySubcontractors { get; set; }

    public virtual DbSet<ComplianceDeadline> ComplianceDeadlines { get; set; }

    public virtual DbSet<ComplianceFiling> ComplianceFilings { get; set; }

    public virtual DbSet<ComplianceSchedule> ComplianceSchedules { get; set; }

    public virtual DbSet<CustomerPayment> CustomerPayments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeEpfo> EmployeeEpfos { get; set; }

    public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

    public virtual DbSet<Epfoecrfile> Epfoecrfiles { get; set; }

    public virtual DbSet<EpfomonthlyWage> EpfomonthlyWages { get; set; }

    public virtual DbSet<Epfoperiod> Epfoperiods { get; set; }

    public virtual DbSet<ExitType> ExitTypes { get; set; }

    public virtual DbSet<FilingStatus> FilingStatuses { get; set; }

    public virtual DbSet<FnFCalculation> FnFCalculations { get; set; }

    public virtual DbSet<GratuityPolicy> GratuityPolicies { get; set; }

    public virtual DbSet<GratuityTransaction> GratuityTransactions { get; set; }

    public virtual DbSet<GstHsnMapping> GstHsnMappings { get; set; }

    public virtual DbSet<GstHsnsac> GstHsnsacs { get; set; }

    public virtual DbSet<GstInvoiceSeries> GstInvoiceSeries { get; set; }

    public virtual DbSet<GstPurchase> GstPurchases { get; set; }

    public virtual DbSet<GstReturn> GstReturns { get; set; }

    public virtual DbSet<GstSale> GstSales { get; set; }

    public virtual DbSet<LeaveEncashmentPolicy> LeaveEncashmentPolicies { get; set; }

    public virtual DbSet<LeaveEncashmentTransaction> LeaveEncashmentTransactions { get; set; }

    public virtual DbSet<LegalDocument> LegalDocuments { get; set; }

    public virtual DbSet<LegalDocumentAcceptance> LegalDocumentAcceptances { get; set; }

    public virtual DbSet<LegalDocumentVersion> LegalDocumentVersions { get; set; }

    public virtual DbSet<LicenseActivation> LicenseActivations { get; set; }

    public virtual DbSet<LicenseAuditLog> LicenseAuditLogs { get; set; }

    public virtual DbSet<LicenseKeyMaster> LicenseKeyMasters { get; set; }

    public virtual DbSet<MachineBinding> MachineBindings { get; set; }

    public virtual DbSet<PartyMaster> PartyMasters { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<PayrollDatum> PayrollData { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<ProductOwner> ProductOwners { get; set; }

    public virtual DbSet<ProductOwnerSubscription> ProductOwnerSubscriptions { get; set; }

    public virtual DbSet<RegisterUser> RegisterUser { get; set; }

    public virtual DbSet<Subcontractor> Subcontractors { get; set; }

    public virtual DbSet<SubcontractorEpfo> SubcontractorEpfos { get; set; }

    public virtual DbSet<SubscriptionInvoice> SubscriptionInvoices { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<TdsCalculation> TdsCalculations { get; set; }

    public virtual DbSet<TdsForm16> TdsForm16s { get; set; }

    public virtual DbSet<TdsForm16A> TdsForm16As { get; set; }

    public virtual DbSet<Tdschallan> Tdschallans { get; set; }

    public virtual DbSet<TdschallanAllocation> TdschallanAllocations { get; set; }

    public virtual DbSet<Tdsdeductee> Tdsdeductees { get; set; }

    public virtual DbSet<Tdsdeductor> Tdsdeductors { get; set; }

    public virtual DbSet<Tdsentry> Tdsentries { get; set; }

    public virtual DbSet<Tdsrate> Tdsrates { get; set; }

    public virtual DbSet<Tdsreturn> Tdsreturns { get; set; }

    public virtual DbSet<TdsreturnChallan> TdsreturnChallans { get; set; }

    public virtual DbSet<TdsreturnEntry> TdsreturnEntries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=38.156.88.238;Database=COMPLYX;User Id=ThiriSQL;Password=ComplyX!1703;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUsers>().ToTable("AspNetUsers");
        modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");

        modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5499A8CF355BBD");

            entity.ToTable("AuditLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.Action)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Details).HasColumnType("text");
            entity.Property(e => e.TargetId).HasColumnName("TargetID");
            entity.Property(e => e.TargetType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AuditLog__UserID__5CD6CB2B");
        });
     
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971C4C24428314");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Domain)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gstin)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GSTIN");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAN");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductOwner).WithMany(p => p.Companies)
                .HasForeignKey(d => d.ProductOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_ProductOwner");
        });

        modelBuilder.Entity<CompanyEpfo>(entity =>
        {
            entity.HasKey(e => e.CompanyEpfoid).HasName("PK__CompanyE__B14AF98729BE1522");

            entity.ToTable("CompanyEPFO");

            entity.Property(e => e.CompanyEpfoid).HasColumnName("CompanyEPFOId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EstablishmentCode).HasMaxLength(50);
            entity.Property(e => e.Extension).HasMaxLength(10);
            entity.Property(e => e.OfficeCode).HasMaxLength(10);

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyEpfos)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CompanyEP__Compa__46B27FE2");
        });

        modelBuilder.Entity<CompanyPartyRole>(entity =>
        {
            entity.HasKey(e => e.CompanyPartyRoleId).HasName("PK__CompanyP__BA10304BBFAF0655");

            entity.ToTable("CompanyPartyRole");

            entity.HasIndex(e => new { e.CompanyId, e.PartyId, e.RoleType }, "UQ_CompanyPartyRole").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleType)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyPartyRoles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyPartyRole_Company");

            entity.HasOne(d => d.Party).WithMany(p => p.CompanyPartyRoles)
                .HasForeignKey(d => d.PartyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyPartyRole_Party");
        });

        modelBuilder.Entity<CompanySubcontractor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyS__3214EC27AB89F9C7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RelationType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubcontractorId).HasColumnName("SubcontractorID");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanySubcontractors)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__CompanySu__Compa__440B1D61");

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.CompanySubcontractors)
                .HasForeignKey(d => d.SubcontractorId)
                .HasConstraintName("FK__CompanySu__Subco__44FF419A");
        });

        modelBuilder.Entity<ComplianceDeadline>(entity =>
        {
            entity.HasKey(e => e.DeadlineId).HasName("PK__Complian__CAF057F788206412");

            entity.Property(e => e.DeadlineId).HasColumnName("DeadlineID");
            entity.Property(e => e.AckNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ComplianceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ComplianceFiling>(entity =>
        {
            entity.HasKey(e => e.FilingId).HasName("PK__Complian__60E4ECFB6C470E93");

            entity.Property(e => e.FilingId).HasColumnName("FilingID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Errors).HasColumnType("text");
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.FilingMonth)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SubmittedAt).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.ComplianceFilings)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Complianc__Compa__5812160E");

            entity.HasOne(d => d.Employee).WithMany(p => p.ComplianceFilings)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Complianc__Emplo__59063A47");
        });

        modelBuilder.Entity<ComplianceSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Complian__9C8A5B693BF7E5EE");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ComplianceType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Frequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OffsetDays).HasDefaultValue(0);
            entity.Property(e => e.StateCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Customer__9B556A58358B3795");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("INR");
            entity.Property(e => e.CustomerIdentifier)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CustomerPayments)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerP__Compa__6A30C649");

            entity.HasOne(d => d.Plan).WithMany(p => p.CustomerPayments)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerP__PlanI__6B24EA82");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1B7724F21");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Aadhaar)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ActiveStatus).HasDefaultValue(true);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Doj).HasColumnName("DOJ");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmploymentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EsicIp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ESIC_IP");
            entity.Property(e => e.ExitReason).HasColumnType("text");
            entity.Property(e => e.ExitType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FatherSpouseName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Grade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAN");
            entity.Property(e => e.PermanentAddress).HasColumnType("text");
            entity.Property(e => e.PfaccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PFAccountNumber");
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PresentAddress).HasColumnType("text");
            entity.Property(e => e.Ptstate)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PTState");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubcontractorId).HasColumnName("SubcontractorID");
            entity.Property(e => e.Uan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("UAN");
            entity.Property(e => e.WorkLocation)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Employees__Compa__4F7CD00D");

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.Employees)
                .HasForeignKey(d => d.SubcontractorId)
                .HasConstraintName("FK__Employees__Subco__5070F446");
        });

        modelBuilder.Entity<EmployeeEpfo>(entity =>
        {
            entity.HasKey(e => e.EmployeeEpfoid).HasName("PK__Employee__F60752E346C728DE");

            entity.ToTable("EmployeeEPFO");

            entity.HasIndex(e => e.Uan, "IX_EmployeeEPFO_UAN");

            entity.HasIndex(e => e.Uan, "UQ__Employee__C5B1D50FD1034894").IsUnique();

            entity.Property(e => e.EmployeeEpfoid).HasColumnName("EmployeeEPFOId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoeEpf).HasColumnName("DOE_EPF");
            entity.Property(e => e.DojEpf).HasColumnName("DOJ_EPF");
            entity.Property(e => e.PfaccountNumber)
                .HasMaxLength(50)
                .HasColumnName("PFAccountNumber");
            entity.Property(e => e.Uan)
                .HasMaxLength(20)
                .HasColumnName("UAN");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeEpfos)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeE__Emplo__4F47C5E3");
        });

        modelBuilder.Entity<EmploymentType>(entity =>
        {
            entity.HasKey(e => e.EmploymentTypeId).HasName("PK__Employme__01754F113796B2E6");

            entity.HasIndex(e => e.Name, "UQ__Employme__737584F6312C0843").IsUnique();

            entity.Property(e => e.EmploymentTypeId).HasColumnName("EmploymentTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Epfoecrfile>(entity =>
        {
            entity.HasKey(e => e.EcrfileId).HasName("PK__EPFOECRF__85A11169225285EE");

            entity.ToTable("EPFOECRFile");

            entity.HasIndex(e => e.WageMonth, "IX_EPFOECRFile_WageMonth");

            entity.Property(e => e.EcrfileId).HasColumnName("ECRFileId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Generated");
            entity.Property(e => e.TotalContribution).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalWages).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WageMonth)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Company).WithMany(p => p.Epfoecrfiles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EPFOECRFi__Compa__5BAD9CC8");

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.Epfoecrfiles)
                .HasForeignKey(d => d.SubcontractorId)
                .HasConstraintName("FK__EPFOECRFi__Subco__5CA1C101");
        });

        modelBuilder.Entity<EpfomonthlyWage>(entity =>
        {
            entity.HasKey(e => e.WageId).HasName("PK__EPFOMont__6CDF5E8A98EA1C4A");

            entity.ToTable("EPFOMonthlyWage");

            entity.HasIndex(e => e.WageMonth, "IX_EPFOMonthlyWage_WageMonth");

            entity.Property(e => e.Contribution).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Edliwages)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("EDLIWages");
            entity.Property(e => e.EmployerShare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Epfwages)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("EPFWages");
            entity.Property(e => e.Epswages)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("EPSWages");
            entity.Property(e => e.Ncpdays)
                .HasDefaultValue(0)
                .HasColumnName("NCPDays");
            entity.Property(e => e.PensionShare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RefundAdvance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WageMonth)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Wages).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.EpfomonthlyWages)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EPFOMonth__Compa__55F4C372");

            entity.HasOne(d => d.Employee).WithMany(p => p.EpfomonthlyWages)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EPFOMonth__Emplo__55009F39");

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.EpfomonthlyWages)
                .HasForeignKey(d => d.SubcontractorId)
                .HasConstraintName("FK__EPFOMonth__Subco__56E8E7AB");
        });

        modelBuilder.Entity<Epfoperiod>(entity =>
        {
            entity.HasKey(e => e.EpfoperiodId).HasName("PK__EPFOPeri__1546C0072615940C");

            entity.ToTable("EPFOPeriod");

            entity.HasIndex(e => new { e.CompanyId, e.SubcontractorId, e.PeriodYear, e.PeriodMonth }, "UQ_Company_Period").IsUnique();

            entity.Property(e => e.EpfoperiodId).HasColumnName("EPFOPeriodId");
            entity.Property(e => e.ChallanFilePath).IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId).HasMaxLength(450);
            entity.Property(e => e.EcrfilePath)
                .IsUnicode(false)
                .HasColumnName("ECRFilePath");
            entity.Property(e => e.LockedAt).HasColumnType("datetime");
            entity.Property(e => e.LockedByUserId).HasMaxLength(450);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Draft");
            entity.Property(e => e.Trrn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TRRN");
            entity.Property(e => e.Trrndate)
                .HasColumnType("datetime")
                .HasColumnName("TRRNDate");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Epfoperiods)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EPFOPeriod_Company");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.EpfoperiodCreatedByUsers)
             .HasForeignKey(d => d.CreatedByUserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Restrict);
            //.HasForeignKey(d => d.CreatedByUserId)
            //.HasConstraintName("FK_EPFOPeriod_AspNetUsers");

            entity.HasOne(d => d.LockedByUser).WithMany(p => p.EpfoperiodLockedByUsers)
                .HasForeignKey(d => d.LockedByUserId)
                .HasConstraintName("FK_EPFOPeriod_LockedByUserId_AspNetUsers");

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.Epfoperiods)
                .HasForeignKey(d => d.SubcontractorId)
                .HasConstraintName("FK_EPFOPeriod_Subcontractor");
        });

        modelBuilder.Entity<ExitType>(entity =>
        {
            entity.HasKey(e => e.ExitTypeId).HasName("PK__ExitType__EF3F71FEC1DA337F");

            entity.HasIndex(e => e.Name, "UQ__ExitType__737584F668391C80").IsUnique();

            entity.Property(e => e.ExitTypeId).HasColumnName("ExitTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FilingStatus>(entity =>
        {
            entity.HasKey(e => e.FilingStatusId).HasName("PK__FilingSt__93F42EE7E95E6711");

            entity.HasIndex(e => e.Name, "UQ__FilingSt__737584F673C6C51B").IsUnique();

            entity.Property(e => e.FilingStatusId).HasColumnName("FilingStatusID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FnFCalculation>(entity =>
        {
            entity.HasKey(e => e.FnFid).HasName("PK__FnF_Calc__00D65052689D4393");

            entity.ToTable("FnF_Calculations");

            entity.Property(e => e.FnFid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FnFID");
            entity.Property(e => e.Bonus).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deductions).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.GratuityAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LeaveEncashmentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetPayable).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.SalaryDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.FnFCalculations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FnF_Calculations_company");
        });

        modelBuilder.Entity<GratuityPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Gratuity__2E133944035BD38C");

            entity.ToTable("Gratuity_Policy");

            entity.Property(e => e.PolicyId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PolicyID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Formula).HasMaxLength(500);
            entity.Property(e => e.MaxGratuityAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.GratuityPolicies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gratuity_Policy_Companies");
        });

        modelBuilder.Entity<GratuityTransaction>(entity =>
        {
            entity.HasKey(e => e.GratuityId).HasName("PK__Gratuity__B18C60476875D6D0");

            entity.ToTable("Gratuity_Transactions");

            entity.Property(e => e.GratuityId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GratuityID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.GratuityAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LastDrawnSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.GratuityTransactions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gratuity_Transactions_company");

            entity.HasOne(d => d.Employee).WithMany(p => p.GratuityTransactions)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gratuity_Transactions_Employees");
        });

        modelBuilder.Entity<GstHsnMapping>(entity =>
        {
            entity.HasKey(e => e.MappingId).HasName("PK__GST_HSN___8B5781BDDF5BFE62");

            entity.ToTable("GST_HSN_Mapping");

            entity.Property(e => e.MappingId).HasColumnName("MappingID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.GstRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("GST_Rate");
            entity.Property(e => e.Hsncode)
                .HasMaxLength(10)
                .HasColumnName("HSNCode");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Saccode)
                .HasMaxLength(10)
                .HasColumnName("SACCode");
        });

        modelBuilder.Entity<GstHsnsac>(entity =>
        {
            entity.HasKey(e => e.CodeId).HasName("PK__GST_HSNS__C6DE2C35222925F2");

            entity.ToTable("GST_HSNSAC");

            entity.Property(e => e.CodeId).HasColumnName("CodeID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CodeType).HasMaxLength(3);
            entity.Property(e => e.GstRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("GST_Rate");
        });

        modelBuilder.Entity<GstInvoiceSeries>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("PK__GST_Invo__F3A1C101807B2BCB");

            entity.ToTable("GST_InvoiceSeries");

            entity.Property(e => e.SeriesId).HasColumnName("SeriesID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CurrentNumber).HasDefaultValue(1);
            entity.Property(e => e.FinancialYear).HasMaxLength(9);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Prefix).HasMaxLength(10);
            entity.Property(e => e.Suffix).HasMaxLength(10);
        });

        modelBuilder.Entity<GstPurchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__GST_Purc__6B0A6BDE1A9BB16E");

            entity.ToTable("GST_Purchase");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.BillNo).HasMaxLength(50);
            entity.Property(e => e.Cgst)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CGST");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Hsncode)
                .HasMaxLength(10)
                .HasColumnName("HSNCode");
            entity.Property(e => e.Igst)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IGST");
            entity.Property(e => e.PlaceOfSupply).HasMaxLength(50);
            entity.Property(e => e.Saccode)
                .HasMaxLength(10)
                .HasColumnName("SACCode");
            entity.Property(e => e.Sgst)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SGST");
            entity.Property(e => e.SupplierGstin)
                .HasMaxLength(15)
                .HasColumnName("SupplierGSTIN");
            entity.Property(e => e.SupplierName).HasMaxLength(200);
            entity.Property(e => e.TaxableValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBillValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<GstReturn>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__GST_Retu__F445E98800177EEA");

            entity.ToTable("GST_Returns");

            entity.Property(e => e.ReturnId).HasColumnName("ReturnID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReturnType).HasMaxLength(10);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");
            entity.Property(e => e.TotalPurchases)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSales)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTaxPaid)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTaxPayable)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<GstSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__GST_Sale__1EE3C41F1134DB22");

            entity.ToTable("GST_Sales");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.Cgst)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CGST");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerGstin)
                .HasMaxLength(15)
                .HasColumnName("CustomerGSTIN");
            entity.Property(e => e.CustomerName).HasMaxLength(200);
            entity.Property(e => e.Hsncode)
                .HasMaxLength(10)
                .HasColumnName("HSNCode");
            entity.Property(e => e.Igst)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IGST");
            entity.Property(e => e.InvoiceNo).HasMaxLength(50);
            entity.Property(e => e.PlaceOfSupply).HasMaxLength(50);
            entity.Property(e => e.Saccode)
                .HasMaxLength(10)
                .HasColumnName("SACCode");
            entity.Property(e => e.Sgst)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SGST");
            entity.Property(e => e.TaxableValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalInvoiceValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<LeaveEncashmentPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Leave_En__2E133944F6A94D91");

            entity.ToTable("Leave_Encashment_Policy");

            entity.Property(e => e.PolicyId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PolicyID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EncashmentFormula).HasMaxLength(500);
            entity.Property(e => e.EncashmentFrequency).HasMaxLength(20);
            entity.Property(e => e.LeaveType).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.LeaveEncashmentPolicies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_encashment_policy_company");
        });

        modelBuilder.Entity<LeaveEncashmentTransaction>(entity =>
        {
            entity.HasKey(e => new { e.EncashmentId, e.CompanyId, e.Employeeid }).HasName("PK__Leave_En__26382D4F9AF4B118");

            entity.ToTable("Leave_Encashment_Transactions");

            entity.Property(e => e.EncashmentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EncashmentID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EncashmentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LeaveType).HasMaxLength(50);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<LegalDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__legalDoc__9666E8AC7747F5CA");

            entity.ToTable("legalDocument");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.ApplicableRegion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("India")
                .HasColumnName("applicable_region");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .HasColumnName("created_by");
            entity.Property(e => e.DocumentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("document_code");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("document_name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ACTIVE")
                .HasColumnName("status");
        });

        modelBuilder.Entity<LegalDocumentAcceptance>(entity =>
        {
            entity.HasKey(e => e.AcceptanceId).HasName("PK__legalDoc__16B6E6B4E2BDABAC");

            entity.ToTable("legalDocumentAcceptance");

            entity.Property(e => e.AcceptanceId).HasColumnName("acceptance_id");
            entity.Property(e => e.AcceptanceMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("acceptance_method");
            entity.Property(e => e.AcceptedAt)
                .HasColumnType("datetime")
                .HasColumnName("accepted_at");
            entity.Property(e => e.AcceptedDevice)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("accepted_device");
            entity.Property(e => e.AcceptedIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("accepted_ip");
            entity.Property(e => e.ConsentProofHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("consent_proof_hash");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("user_id");
            entity.Property(e => e.VersionId).HasColumnName("version_id");

            entity.HasOne(d => d.Document).WithMany(p => p.LegalDocumentAcceptances)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__legalDocu__docum__0AF29B96");

            entity.HasOne(d => d.Version).WithMany(p => p.LegalDocumentAcceptances)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__legalDocu__versi__0BE6BFCF");
        });

        modelBuilder.Entity<LegalDocumentVersion>(entity =>
        {
            entity.HasKey(e => e.VersionId).HasName("PK__legalDoc__07A5886958E5206D");

            entity.ToTable("legalDocumentVersion");

            entity.Property(e => e.VersionId).HasColumnName("version_id");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("datetime")
                .HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(450)
                .HasColumnName("approved_by");
            entity.Property(e => e.ChangeSummary).HasColumnName("change_summary");
            entity.Property(e => e.ContentHash)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("content_hash");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(450)
                .HasColumnName("created_by");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.EffectiveFromDate).HasColumnName("effective_from_date");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.HtmlContent).HasColumnName("html_content");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.IsPublished)
                .HasDefaultValue(false)
                .HasColumnName("is_published");
            entity.Property(e => e.LegalBasis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("legal_basis");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.VersionNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("version_number");
            entity.Property(e => e.VersionType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("version_type");

            entity.HasOne(d => d.Document).WithMany(p => p.LegalDocumentVersions)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__legalDocu__docum__04459E07");
        });

        modelBuilder.Entity<LicenseActivation>(entity =>
        {
            entity.HasKey(e => e.ActivationId).HasName("PK__LicenseA__379D341AB8E55494");

            entity.ToTable("LicenseActivation");

            entity.Property(e => e.ActivatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AppVersion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GraceExpiryAt).HasColumnType("datetime");
            entity.Property(e => e.LastVerifiedAt).HasColumnType("datetime");
            entity.Property(e => e.MachineHash)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.MachineName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OsUser)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.License).WithMany(p => p.LicenseActivations)
                .HasForeignKey(d => d.LicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LicenseActivation_License");
        });

        modelBuilder.Entity<LicenseAuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__LicenseA__A17F239816DB30FF");

            entity.Property(e => e.EventMessage).IsUnicode(false);
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoggedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MachineHash)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LicenseKeyMaster>(entity =>
        {
            entity.HasKey(e => e.LicenseId).HasName("PK__LicenseK__72D60082C9E34BDB");

            entity.ToTable("LicenseKeyMaster");

            entity.HasIndex(e => e.LicenseKey, "UQ__LicenseK__45E1DD6F0F26DD56").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LicenseKey)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PlanType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.ProductOwner).WithMany(p => p.LicenseKeyMasters)
                .HasForeignKey(d => d.ProductOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LicenseKeyMaster_ProductOwner");
        });

        modelBuilder.Entity<MachineBinding>(entity =>
        {
            entity.HasKey(e => e.MachineBindingId).HasName("PK__MachineB__CE63017D1AA47129");

            entity.ToTable("MachineBinding");

            entity.HasIndex(e => e.MachineHash, "UQ__MachineB__FEB3E2CA14735FC3").IsUnique();

            entity.Property(e => e.Cpuid)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CPUId");
            entity.Property(e => e.FirstSeenAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastSeenAt).HasColumnType("datetime");
            entity.Property(e => e.Macaddresses)
                .IsUnicode(false)
                .HasColumnName("MACAddresses");
            entity.Property(e => e.MachineHash)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.MotherboardSerial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.WindowsSid)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("WindowsSID");

            entity.HasOne(d => d.LicenseActivation).WithMany(p => p.MachineBindings)
                .HasForeignKey(d => d.LicenseActivationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_machinebinding_licenseActivation");
        });

        modelBuilder.Entity<PartyMaster>(entity =>
        {
            entity.HasKey(e => e.PartyId).HasName("PK__PartyMas__1640CD3326152C43");

            entity.ToTable("PartyMaster");

            entity.Property(e => e.Address1).HasMaxLength(250);
            entity.Property(e => e.Address2).HasMaxLength(250);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Gstin)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GSTIN");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAN");
            entity.Property(e => e.PartyName).HasMaxLength(200);
            entity.Property(e => e.PartyType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__PaymentT__55433A4B5F0B722F");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Fees).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Gateway)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GatewayPaymentId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentTr__Payme__6EF57B66");
        });

        modelBuilder.Entity<PayrollDatum>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("PK__PayrollD__99DFC69261FBB438");

            entity.Property(e => e.PayrollId).HasColumnName("PayrollID");
            entity.Property(e => e.BankAccount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Basic).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Esi)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ESI");
            entity.Property(e => e.GrossSalary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Hra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("HRA");
            entity.Property(e => e.Ifsc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSC");
            entity.Property(e => e.Month)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.NetPay).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Pf)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PF");
            entity.Property(e => e.ProfessionalTax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SpecialAllowance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tds)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TDS");
            entity.Property(e => e.VariablePay).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.PayrollData)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__PayrollDa__Emplo__5441852A");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__755C22D734917A04");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.BillingCycle)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.MultiOrg).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProductOwner>(entity =>
        {
            entity.HasKey(e => e.ProductOwnerId).HasName("PK__AccountO__CB75F2FEE51AFD84");

            entity.HasIndex(e => e.Email, "UQ__AccountO__A9D105342D00F240").IsUnique();

            entity.Property(e => e.AllowCloudBackup).HasDefaultValue(false);
            entity.Property(e => e.AllowClramodule)
                .HasDefaultValue(false)
                .HasColumnName("AllowCLRAModule");
            entity.Property(e => e.AllowDscsigning)
                .HasDefaultValue(false)
                .HasColumnName("AllowDSCSigning");
            entity.Property(e => e.AllowGstmodule)
                .HasDefaultValue(false)
                .HasColumnName("AllowGSTModule");
            entity.Property(e => e.AllowPayrollModule).HasDefaultValue(false);
            entity.Property(e => e.AllowTdsmodule)
                .HasDefaultValue(false)
                .HasColumnName("AllowTDSModule");
            entity.Property(e => e.City).HasMaxLength(150);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasDefaultValue("India");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LegalName).HasMaxLength(200);
            entity.Property(e => e.MaxCompanies).HasDefaultValue(1);
            entity.Property(e => e.MaxStorageMb)
                .HasDefaultValue(500)
                .HasColumnName("MaxStorageMB");
            entity.Property(e => e.MaxUsers).HasDefaultValue(1);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.OrganizationName).HasMaxLength(200);
            entity.Property(e => e.OrganizationType).HasMaxLength(100);
            entity.Property(e => e.OwnerName).HasMaxLength(150);
            entity.Property(e => e.Pincode).HasMaxLength(10);
            entity.Property(e => e.RegistrationId).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(150);
            entity.Property(e => e.SubscriptionExpiry).HasDefaultValueSql("(dateadd(year,(1),getdate()))");
            entity.Property(e => e.SubscriptionPlan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Basic");
            entity.Property(e => e.SubscriptionStart).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductOwnerSubscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__AccountO__9A2B249DD72E3A25");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsTrial).HasDefaultValue(false);
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TransactionId).HasMaxLength(200);

            entity.HasOne(d => d.Plan).WithMany(p => p.ProductOwnerSubscriptions)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountOw__PlanI__3EDC53F0");

            entity.HasOne(d => d.ProductOwner).WithMany(p => p.ProductOwnerSubscriptions)
                .HasForeignKey(d => d.ProductOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountOw__Produ__3DE82FB7");
        });

        modelBuilder.Entity<RegisterUser>(entity =>
        {

            entity.HasKey(x => x.UserID);

            entity.Property(x => x.UserID)
                .HasDefaultValueSql("NEWID()");
            // .ToTable("RegisterUser");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Domain).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gstin)
                .HasMaxLength(50)
                .HasColumnName("GSTIN");
            entity.Property(e => e.Pan)
                .HasMaxLength(50)
                .HasColumnName("PAN");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Subcontractor>(entity =>
        {
            entity.HasKey(e => e.SubcontractorId).HasName("PK__Subcontr__E20BBB13D4F123D4");

            entity.Property(e => e.SubcontractorId).HasColumnName("SubcontractorID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gstin)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("GSTIN");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAN");

            entity.HasOne(d => d.Company).WithMany(p => p.Subcontractors)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubContractors_Companies");
        });

        modelBuilder.Entity<SubcontractorEpfo>(entity =>
        {
            entity.HasKey(e => e.SubcontractorEpfoid).HasName("PK__Subcontr__2AEAB4CE36E1343B");

            entity.ToTable("SubcontractorEPFO");

            entity.Property(e => e.SubcontractorEpfoid).HasColumnName("SubcontractorEPFOId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EstablishmentCode).HasMaxLength(50);
            entity.Property(e => e.Extension).HasMaxLength(10);
            entity.Property(e => e.OfficeCode).HasMaxLength(10);

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.SubcontractorEpfos)
                .HasForeignKey(d => d.SubcontractorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subcontra__Subco__4A8310C6");
        });

        modelBuilder.Entity<SubscriptionInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Subscrip__D796AAD5A98764C1");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("INR");
            entity.Property(e => e.PaidOn).HasColumnType("datetime");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.SubscriptionInvoices)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Compa__73BA3083");

            entity.HasOne(d => d.Payment).WithMany(p => p.SubscriptionInvoices)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Subscript__Payme__74AE54BC");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Subscrip__755C22B71944A259");

            entity.HasIndex(e => e.PlanCode, "UQ__Subscrip__DDC8069B7FFEB2EB").IsUnique();

            entity.Property(e => e.AllowCloudBackup).HasDefaultValue(false);
            entity.Property(e => e.AllowClra)
                .HasDefaultValue(false)
                .HasColumnName("AllowCLRA");
            entity.Property(e => e.AllowDscsigning)
                .HasDefaultValue(false)
                .HasColumnName("AllowDSCSigning");
            entity.Property(e => e.AllowEpfo)
                .HasDefaultValue(true)
                .HasColumnName("AllowEPFO");
            entity.Property(e => e.AllowEsic)
                .HasDefaultValue(true)
                .HasColumnName("AllowESIC");
            entity.Property(e => e.AllowGst)
                .HasDefaultValue(false)
                .HasColumnName("AllowGST");
            entity.Property(e => e.AllowLwf)
                .HasDefaultValue(true)
                .HasColumnName("AllowLWF");
            entity.Property(e => e.AllowPayroll).HasDefaultValue(false);
            entity.Property(e => e.AllowPt)
                .HasDefaultValue(true)
                .HasColumnName("AllowPT");
            entity.Property(e => e.AllowTds)
                .HasDefaultValue(false)
                .HasColumnName("AllowTDS");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxStorageMb).HasColumnName("MaxStorageMB");
            entity.Property(e => e.PlanCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlanName).HasMaxLength(200);
            entity.Property(e => e.PriceMonthly).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PriceYearly).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TdsCalculation>(entity =>
        {
            entity.HasKey(e => e.TdscalcId).HasName("PK__TDS_Calc__D53E78BC43B46B00");

            entity.ToTable("TDS_Calculation");

            entity.Property(e => e.TdscalcId).HasColumnName("TDSCalcID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ExemptionAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinancialYear)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IncomeAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PAN");
            entity.Property(e => e.TaxableAmount)
                .HasComputedColumnSql("([IncomeAmount]-[ExemptionAmount])", true)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.Tdsamount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TDSAmount");
            entity.Property(e => e.Tdsrate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("TDSRate");
            entity.Property(e => e.Tdssection)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TDSSection");
        });

        modelBuilder.Entity<TdsForm16>(entity =>
        {
            entity.HasKey(e => e.Form16Id).HasName("PK__TDS_Form__A9EE58BD562F2421");

            entity.ToTable("TDS_Form16");

            entity.Property(e => e.Form16Id).HasColumnName("Form16ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FinancialYear)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GeneratedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Pdfpath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PDFPath");
        });

        modelBuilder.Entity<TdsForm16A>(entity =>
        {
            entity.HasKey(e => e.Form16Aid).HasName("PK__TDS_Form__70E5098C123750A4");

            entity.ToTable("TDS_Form16A");

            entity.Property(e => e.Form16Aid).HasColumnName("Form16AID");
            entity.Property(e => e.FinancialYear)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GeneratedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Pdfpath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PDFPath");
            entity.Property(e => e.Quarter)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.VendorId).HasColumnName("VendorID");
        });

        modelBuilder.Entity<Tdschallan>(entity =>
        {
            entity.HasKey(e => e.ChallanId).HasName("PK__TDSChall__BFB04324A2747D84");

            entity.ToTable("TDSChallan");

            entity.HasIndex(e => new { e.Bsrcode, e.ChallanDate, e.ChallanSerialNo }, "UQ_TDSChallan_OLTAS").IsUnique();

            entity.Property(e => e.Bsrcode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BSRCode");
            entity.Property(e => e.ChallanSerialNo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InterestAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LateFeeAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MatchedWithOltas).HasColumnName("MatchedWithOLTAS");
            entity.Property(e => e.OtherAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SectionCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount)
                .HasComputedColumnSql("((([TaxAmount]+[InterestAmount])+[LateFeeAmount])+[OtherAmount])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.Deductor).WithMany(p => p.Tdschallans)
                .HasForeignKey(d => d.DeductorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSChallan_Deductor");
        });

        modelBuilder.Entity<TdschallanAllocation>(entity =>
        {
            entity.HasKey(e => e.AllocationId).HasName("PK__TDSChall__B3C6D64B3174909D");

            entity.ToTable("TDSChallanAllocation");

            entity.HasIndex(e => e.EntryId, "UQ_TDSChallanAllocation").IsUnique();

            entity.Property(e => e.AllocatedTdsamount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("AllocatedTDSAmount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.HasOne(d => d.Challan).WithMany(p => p.TdschallanAllocations)
                .HasForeignKey(d => d.ChallanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSChallanAllocation_Challan");

            entity.HasOne(d => d.Entry).WithOne(p => p.TdschallanAllocation)
                .HasForeignKey<TdschallanAllocation>(d => d.EntryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSChallanAllocation_Entry");
        });

        modelBuilder.Entity<Tdsdeductee>(entity =>
        {
            entity.HasKey(e => e.DeducteeId).HasName("PK__TDSDeduc__B9BC247CE53C3040");

            entity.ToTable("TDSDeductee");

            entity.HasIndex(e => e.CompanyId, "IX_Deductee_CompanyID");

            entity.HasIndex(e => e.IsActive, "IX_Deductee_IsActive");

            entity.HasIndex(e => e.Pan, "IX_Deductee_PAN");

            entity.HasIndex(e => e.DeducteeType, "IX_Deductee_Type");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.DeducteeName).HasMaxLength(200);
            entity.Property(e => e.DeducteeType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAN");
            entity.Property(e => e.Panstatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("UNKNOWN")
                .HasColumnName("PANStatus");
            entity.Property(e => e.ResidentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("RESIDENT");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.Tdsdeductees)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deductee_Company");
        });

        modelBuilder.Entity<Tdsdeductor>(entity =>
        {
            entity.HasKey(e => e.DeductorId).HasName("PK__TDSDeduc__602ECD2377ED156E");

            entity.ToTable("TDSDeductor");

            entity.HasIndex(e => e.DeductorCategory, "IX_Deductor_Category");

            entity.HasIndex(e => e.CompanyId, "IX_Deductor_CompanyID");

            entity.HasIndex(e => e.IsActive, "IX_Deductor_IsActive");

            entity.HasIndex(e => e.Tan, "UQ_Deductor_TAN").IsUnique();

            entity.Property(e => e.Address1).HasMaxLength(250);
            entity.Property(e => e.Address2).HasMaxLength(250);
            entity.Property(e => e.Aocode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AOCode");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.DeductorCategory)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeductorName).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Pan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PAN");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Tan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TAN");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.HasOne(d => d.Company).WithMany(p => p.Tdsdeductors)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deductor_Company");
        });

        modelBuilder.Entity<Tdsentry>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__TDSEntry__F57BD2F7FB5A8C45");

            entity.ToTable("TDSEntry");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Cess)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HigherRateReason)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SectionCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Surcharge)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxableAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tdsamount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TDSAmount");
            entity.Property(e => e.Tdsrate)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("TDSRate");
            entity.Property(e => e.TotalTds)
                .HasComputedColumnSql("(([TDSAmount]+[Surcharge])+[Cess])", true)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("TotalTDS");

            entity.HasOne(d => d.Deductee).WithMany(p => p.Tdsentries)
                .HasForeignKey(d => d.DeducteeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSEntry_Deductee");

            entity.HasOne(d => d.Deductor).WithMany(p => p.Tdsentries)
                .HasForeignKey(d => d.DeductorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSEntry_Deductor");
        });

        modelBuilder.Entity<Tdsrate>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("PK__TDSRates__711BE0AC9EF99D39");

            entity.ToTable("TDSRates");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Rate).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.TaxName).HasMaxLength(150);
            entity.Property(e => e.TaxType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        });

        modelBuilder.Entity<Tdsreturn>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__TDSRetur__F445E9A861EAE79F");

            entity.ToTable("TDSReturn");

            entity.HasIndex(e => e.DeductorId, "IX_TDSReturn_DeductorId");

            entity.HasIndex(e => new { e.FinancialYear, e.Quarter }, "IX_TDSReturn_FY_QTR");

            entity.HasIndex(e => e.FormType, "IX_TDSReturn_FormType");

            entity.HasIndex(e => e.Status, "IX_TDSReturn_Status");

            entity.HasIndex(e => new { e.DeductorId, e.FormType, e.FinancialYear, e.Quarter }, "UQ_TDSReturn").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.FinancialYear)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FormType)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Fvuversion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FVUVersion");
            entity.Property(e => e.OriginalTokenNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Quarter)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReturnType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("REGULAR");
            entity.Property(e => e.Rpuversion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RPUVersion");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("DRAFT");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.HasOne(d => d.Deductor).WithMany(p => p.Tdsreturns)
                .HasForeignKey(d => d.DeductorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSReturn_Deductor");
        });

        modelBuilder.Entity<TdsreturnChallan>(entity =>
        {
            entity.HasKey(e => e.ReturnChallanId).HasName("PK__TDSRetur__E9F31862F8127EC5");

            entity.ToTable("TDSReturnChallan");

            entity.HasIndex(e => new { e.ReturnId, e.ChallanId }, "UQ_TDSReturnChallan").IsUnique();

            entity.HasOne(d => d.Challan).WithMany(p => p.TdsreturnChallans)
                .HasForeignKey(d => d.ChallanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSReturnChallan_Challan");

            entity.HasOne(d => d.Return).WithMany(p => p.TdsreturnChallans)
                .HasForeignKey(d => d.ReturnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSReturnChallan_Return");
        });

        modelBuilder.Entity<TdsreturnEntry>(entity =>
        {
            entity.HasKey(e => e.ReturnEntryId).HasName("PK__TDSRetur__4DC4ABC1BA67B9B3");

            entity.ToTable("TDSReturnEntry");

            entity.HasIndex(e => new { e.ReturnId, e.EntryId }, "UQ_TDSReturnEntry").IsUnique();

            entity.HasOne(d => d.Entry).WithMany(p => p.TdsreturnEntries)
                .HasForeignKey(d => d.EntryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSReturnEntry_Entry");

            entity.HasOne(d => d.Return).WithMany(p => p.TdsreturnEntries)
                .HasForeignKey(d => d.ReturnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDSReturnEntry_Return");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACEA58FDA3");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053428C779EF").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubcontractorId).HasColumnName("SubcontractorID");

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Users__CompanyID__4AB81AF0");

            entity.HasOne(d => d.Subcontractor).WithMany(p => p.Users)
                .HasForeignKey(d => d.SubcontractorId)
                .HasConstraintName("FK__Users__Subcontra__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
