using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Domain { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public string? State { get; set; }

    public string? Gstin { get; set; }

    public string? Pan { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? MaxEmployees { get; set; }

    public int ProductOwnerId { get; set; }

    public virtual ICollection<CompanyEpfo> CompanyEpfos { get; set; } = new List<CompanyEpfo>();

    public virtual ICollection<CompanyPartyRole> CompanyPartyRoles { get; set; } = new List<CompanyPartyRole>();

    public virtual ICollection<CompanySubcontractor> CompanySubcontractors { get; set; } = new List<CompanySubcontractor>();

    public virtual ICollection<ComplianceFiling> ComplianceFilings { get; set; } = new List<ComplianceFiling>();

    public virtual ICollection<CustomerPayment> CustomerPayments { get; set; } = new List<CustomerPayment>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Epfoecrfile> Epfoecrfiles { get; set; } = new List<Epfoecrfile>();

    public virtual ICollection<EpfomonthlyWage> EpfomonthlyWages { get; set; } = new List<EpfomonthlyWage>();

    public virtual ICollection<Epfoperiod> Epfoperiods { get; set; } = new List<Epfoperiod>();

    public virtual ICollection<FnFCalculation> FnFCalculations { get; set; } = new List<FnFCalculation>();

    public virtual ICollection<GratuityPolicy> GratuityPolicies { get; set; } = new List<GratuityPolicy>();

    public virtual ICollection<GratuityTransaction> GratuityTransactions { get; set; } = new List<GratuityTransaction>();

    public virtual ICollection<LeaveEncashmentPolicy> LeaveEncashmentPolicies { get; set; } = new List<LeaveEncashmentPolicy>();

    public virtual ProductOwner ProductOwner { get; set; } = null!;

    public virtual ICollection<Subcontractor> Subcontractors { get; set; } = new List<Subcontractor>();

    public virtual ICollection<SubscriptionInvoice> SubscriptionInvoices { get; set; } = new List<SubscriptionInvoice>();

    public virtual ICollection<Tdsdeductee> Tdsdeductees { get; set; } = new List<Tdsdeductee>();

    public virtual ICollection<Tdsdeductor> Tdsdeductors { get; set; } = new List<Tdsdeductor>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
