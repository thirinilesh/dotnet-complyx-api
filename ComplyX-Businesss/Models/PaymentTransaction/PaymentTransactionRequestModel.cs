using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.PaymentTransaction
{
    public class PaymentTransactionRequestModel
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

    }
}
