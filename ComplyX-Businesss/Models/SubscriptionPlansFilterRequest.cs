using System.ComponentModel.DataAnnotations;

namespace ComplyX_Businesss.Models
{
    public class SubscriptionPlansFilterRequest
    {
        [Key]
            public int? PlanId { get; set; }
            public string? PlanName { get; set; }
            public string? PlanCode { get; set; }

    }
}
