using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class GST_InvoiceSeries
    {
        public int SeriesID { get; set; }
        public int CompanyID { get; set; }
        public string FinancialYear {  get; set; }
        public string? Prefix { get; set; }
        public int? CurrentNumber { get; set; }
        public string? Suffix { get; set; }
        public DateTime? LastUpdated { get; set; }
        public virtual Company? Company { get; set; }
    }
}
