using System.ComponentModel.DataAnnotations;

namespace ComplyX.Models
{
    public class ProductOwnerSubscriptions
    {
        [Key]
        public int SubscriptionId { get; set; }
        public int? ProductOwnerId { get; set; }     
        public int? PlanId { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public bool IsTrial { get; set; } = false;
        public string PaymentMode { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }  
        public string TransactionId { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;

        public virtual ProductOwners ProductOwners { get; set; }
        public virtual SubscriptionPlans SubscriptionPlans { get; set; }
    }
}
