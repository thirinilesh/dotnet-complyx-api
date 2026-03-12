using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Get_Sales_Items
{
    public class GstSalesItemsResponseModel
    {
        public int ItemID { get; set; }
        public string InvoiceNo { get; set; }
        public int CompanyId { get; set; }
        public string? ItemName { get; set; }
        public string? HSNCode { get; set; }
        public string? SACCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TaxableValue { get; set; }
        public decimal? GSTRate { get; set; }
        public decimal? CGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal? SGST { get; set; }
        public decimal? TotalItemValue { get; set; }
    }
}
