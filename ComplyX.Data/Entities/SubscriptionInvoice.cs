using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class SubscriptionInvoice
{
    public int InvoiceId { get; set; }

    public int CompanyId { get; set; }

    public int? PaymentId { get; set; }

    public DateOnly? PeriodStart { get; set; }

    public DateOnly? PeriodEnd { get; set; }

    public decimal? Amount { get; set; }

    public string? Currency { get; set; }

    public DateTime? PaidOn { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual CustomerPayment? Payment { get; set; }
}
