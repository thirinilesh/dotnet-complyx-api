using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class PaymentTransactions
    {
        [Key]
        public int TransactionID { get; set; }
        public int PaymentID { get; set; }
        public string  Gateway { get; set; }
        public string? GatewayPaymentId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Fees { get; set; }
        public string? Status { get; set; }
        public string? ResponsePayload { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual CustomerPayments? CustomerPayments { get; set; }


    }
}
