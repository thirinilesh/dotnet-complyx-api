using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class Leave_Encashment_Transactions
    {
        [Key]
        public Guid EncashmentID { get; set; }
        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public string? LeaveType { get; set; }
        public int? DaysEncashed { get; set; }
        public decimal? EncashmentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public  DateTime UpdatedAt { get; set; }
    }
}
