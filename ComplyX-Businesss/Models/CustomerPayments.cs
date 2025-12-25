using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class CustomerPayments
    {
        [Key]
        public int PaymentID { get; set; }
        public int CompanyID { get; set; }
        public string? CustomerIdentifier { get; set; }
        public int PlanID { get; set; }
        public decimal Amount { get; set; }
        public string? Currency {  get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Plans? Plans { get; set; }
        public virtual ICollection<SubscriptionInvoices>? SubscriptionInvoices { get; set; } = new List<SubscriptionInvoices>();
        public virtual ICollection<PaymentTransactions>? PaymentTransactions { get; set; } = new List<PaymentTransactions>();

    }
}
