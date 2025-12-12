using System.ComponentModel.DataAnnotations;

namespace ComplyX.Models
{
    public class SubscriptionPlans
    {
        [Key]
        public int PlanId { get; set; }
        public string PlanCode { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceMonthly { get; set; }  
        public decimal PriceYearly { get; set; }  
        public int MaxCompanies { get; set; } 
        public int MaxUsers { get; set;}  
        public int MaxStorageMB { get; set; }
        public bool AllowEPFO {  get; set; }
        public bool AllowESIC { get; set; }
        public bool AllowGST { get; set; }
        public bool AllowTDS { get; set; }
        public bool AllowCLRA { get; set; }
        public bool AllowLWF { get; set; }
        public bool AllowPT { get; set; }
        public bool AllowPayroll { get; set; }
        public bool AllowDSCSigning { get; set; }
        public bool AllowCloudBackup { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ProductOwnerSubscriptions> ProductOwnerSubscriptionss { get; set; } = new List<ProductOwnerSubscriptions>();
    }
}
