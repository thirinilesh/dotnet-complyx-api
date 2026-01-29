using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class PaymentTransaction
{
    public int TransactionId { get; set; }

    public int PaymentId { get; set; }

    public string Gateway { get; set; } = null!;

    public string? GatewayPaymentId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Fees { get; set; }

    public string? Status { get; set; }

    public string? ResponsePayload { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual CustomerPayment Payment { get; set; } = null!;
}
