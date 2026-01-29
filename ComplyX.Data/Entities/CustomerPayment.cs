using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class CustomerPayment
{
    public int PaymentId { get; set; }

    public int CompanyId { get; set; }

    public string? CustomerIdentifier { get; set; }

    public int PlanId { get; set; }

    public decimal Amount { get; set; }

    public string? Currency { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual Plan Plan { get; set; } = null!;

    public virtual ICollection<SubscriptionInvoice> SubscriptionInvoices { get; set; } = new List<SubscriptionInvoice>();
}
