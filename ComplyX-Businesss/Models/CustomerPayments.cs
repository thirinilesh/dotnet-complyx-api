using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonIgnore]
        public virtual Company? Company { get; set; }
        [JsonIgnore]
        public virtual Plans? Plans { get; set; }
        [JsonIgnore]
        public virtual ICollection<SubscriptionInvoices>? SubscriptionInvoices { get; set; } = new List<SubscriptionInvoices>();
        [JsonIgnore]
        public virtual ICollection<PaymentTransactions>? PaymentTransactions { get; set; } = new List<PaymentTransactions>();

    }
}
