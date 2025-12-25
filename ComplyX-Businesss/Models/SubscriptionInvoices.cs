using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class SubscriptionInvoices
    {
        [Key]
        public int InvoiceID {  get; set; }
        public int CompanyID { get; set; }
        public int? PaymentID { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public  decimal? Amount { get; set; }
        public string? Currency {  get; set; }
        public DateTime? PaidOn { get; set; }
        public string? Status {  get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Company? Company { get; set; }

        public virtual CustomerPayments? CustomerPayments { get; set; }
    }
}
