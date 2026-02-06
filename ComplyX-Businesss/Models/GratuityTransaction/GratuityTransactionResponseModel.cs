using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GratuityTransaction
{
    public class GratuityTransactionResponseModel
    {
        public Guid GratuityId { get; set; }

        public decimal? LastDrawnSalary { get; set; }

        public int? YearsOfService { get; set; }

        public decimal? GratuityAmount { get; set; }

        public string? PaymentStatus { get; set; }

        public DateOnly? PaidDate { get; set; }

        public Guid? ApprovedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int EmployeeId { get; set; }

        public int CompanyId { get; set; }
    }
}
