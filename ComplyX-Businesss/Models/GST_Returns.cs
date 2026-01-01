using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class GST_Returns
    {
        public int ReturnID { get; set; }
        public int CompanyID { get; set; }
        public string ReturnType { get; set; }
        public int PeriodMonth { get; set; }
        public int PeriodYear { get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? TotalPurchases { get; set; }
        public decimal? TotalTaxPayable { get; set; }
        public decimal? TotalTaxPaid { get; set; }
        public DateTime? FilingDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public virtual Company? Company { get; set; }
    }
}
