using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTSales
{
    public class GstSaleResponseModel
    {
        public int SaleId { get; set; }

        public int CompanyId { get; set; }

        public string InvoiceNo { get; set; } = null!;

        public DateOnly InvoiceDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerGstin { get; set; }

        public string? PlaceOfSupply { get; set; }

        public string? Hsncode { get; set; }

        public string? Saccode { get; set; }

        public decimal TaxableValue { get; set; }

        public decimal? Cgst { get; set; }

        public decimal? Sgst { get; set; }

        public decimal? Igst { get; set; }

        public decimal TotalInvoiceValue { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }
    }
}
