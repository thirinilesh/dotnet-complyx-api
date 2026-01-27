using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class GST_Sales
    {
        public int SaleID { get; set; }
        public int CompanyID { get; set; }
        public string  InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerGSTIN {  get; set; }
        public string? PLaceOfSupply { get; set; }
        public string? HSNCode { get; set; }
        public string? SACCode { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
    }
 
}
