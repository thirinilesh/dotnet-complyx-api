using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.SubscriptionInvoices
{
    public class SubscriptionInvoicesRequestModel
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

    }
}
