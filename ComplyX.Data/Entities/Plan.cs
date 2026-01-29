using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Plan
{
    public int PlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? MaxEmployees { get; set; }

    public bool? MultiOrg { get; set; }

    public decimal? Price { get; set; }

    public string? BillingCycle { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<CustomerPayment> CustomerPayments { get; set; } = new List<CustomerPayment>();
}
