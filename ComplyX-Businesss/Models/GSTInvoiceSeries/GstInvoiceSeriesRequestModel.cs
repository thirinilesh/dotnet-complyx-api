using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTInvoiceSeries
{
    public class GstInvoiceSeriesRequestModel
    {
        public int SeriesId { get; set; }

        public int CompanyId { get; set; }

        public string FinancialYear { get; set; } = null!;

        public string? Prefix { get; set; }

        public int? CurrentNumber { get; set; }

        public string? Suffix { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
