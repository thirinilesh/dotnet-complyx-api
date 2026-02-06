using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LeaveEncashmentTransaction
{
    public class LeaveEncashmentTransactionResponseModel
    {
        public Guid EncashmentId { get; set; }

        public int CompanyId { get; set; }

        public int Employeeid { get; set; }

        public string? LeaveType { get; set; }

        public int? DaysEncashed { get; set; }

        public decimal? EncashmentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Status { get; set; }

        public Guid? ApprovedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
