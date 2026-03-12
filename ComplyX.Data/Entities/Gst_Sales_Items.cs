using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Data.Entities
{
    public partial class Gst_Sales_Items
    {
        [Key]
        public int ItemID { get; set; }
        public string InvoiceNo { get; set; }
        public int CompanyId { get; set; }
        public string? ItemName { get; set; }
        public string? HSNCode { get; set; }
        public string? SACCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TaxableValue { get; set; }
        public decimal? GSTRate {  get; set; }
        public decimal? CGST { get; set; }
        public decimal? IGST {  get; set; }
        public decimal? SGST { get; set; }
        public decimal? TotalItemValue { get; set; }
        public virtual Company Company { get; set; } = null!;
        public virtual GST_Sales_Invoice GST_Sales_Invoice { get; set; } = null!;
    }
}
