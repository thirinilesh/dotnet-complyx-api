using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Subcontractor
{
    public int SubcontractorId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public string? Gstin { get; set; }

    public string? Pan { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CompanySubcontractor> CompanySubcontractors { get; set; } = new List<CompanySubcontractor>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Epfoecrfile> Epfoecrfiles { get; set; } = new List<Epfoecrfile>();

    public virtual ICollection<EpfomonthlyWage> EpfomonthlyWages { get; set; } = new List<EpfomonthlyWage>();

    public virtual ICollection<Epfoperiod> Epfoperiods { get; set; } = new List<Epfoperiod>();

    public virtual ICollection<SubcontractorEpfo> SubcontractorEpfos { get; set; } = new List<SubcontractorEpfo>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
