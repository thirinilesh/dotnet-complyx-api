using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTSales
{
    public class GstSaleRequestModel
    {
        public int SaleId { get; set; }

        public int CompanyId { get; set; }

        public string InvoiceNo { get; set; } = null!;

        public DateOnly InvoiceDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerGstin { get; set; }

        public string? PlaceOfSupply { get; set; }    
        public decimal TotalInvoiceValue { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

    }
     
}
