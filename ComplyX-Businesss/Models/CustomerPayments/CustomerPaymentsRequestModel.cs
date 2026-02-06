using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.CustomerPayments
{
    public class CustomerPaymentsRequestModel
    {
        public int PaymentId { get; set; }

        public int CompanyId { get; set; }

        public string? CustomerIdentifier { get; set; }

        public int PlanId { get; set; }

        public decimal Amount { get; set; }

        public string? Currency { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
