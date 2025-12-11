namespace ComplyX.Models
{
    public class ProductOwnerSubscriptionDto
    {
        public int? ProductOwnerId { get; set; }
        public int? PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string IsTrial { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string PlanCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceMonthly { get; set; }
        public decimal PriceYearly { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
