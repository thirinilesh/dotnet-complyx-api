using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class ProductOwnerSubscription
{
    public int SubscriptionId { get; set; }

    public int ProductOwnerId { get; set; }

    public int PlanId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool? IsTrial { get; set; }

    public string? PaymentMode { get; set; }

    public decimal? AmountPaid { get; set; }

    public string? TransactionId { get; set; }

    public string? Remarks { get; set; }

    public virtual SubscriptionPlan Plan { get; set; } = null!;

    public virtual ProductOwner ProductOwner { get; set; } = null!;
}
