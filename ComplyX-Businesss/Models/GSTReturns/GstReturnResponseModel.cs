using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTReturns
{
    public class GstReturnResponseModel
    {
        public int ReturnId { get; set; }

        public int CompanyId { get; set; }

        public string ReturnType { get; set; } = null!;

        public int PeriodMonth { get; set; }

        public int PeriodYear { get; set; }

        public decimal? TotalSales { get; set; }

        public decimal? TotalPurchases { get; set; }

        public decimal? TotalTaxPayable { get; set; }

        public decimal? TotalTaxPaid { get; set; }

        public DateOnly? FilingDate { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
