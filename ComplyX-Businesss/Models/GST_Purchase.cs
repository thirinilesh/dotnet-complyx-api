using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class GST_Purchase
    {
        public int PurchaseID { get; set; }
        public int CompanyID { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierGSTIN { get; set; }
        public string? PLaceOfSupply { get; set; }
        public string? HSNCode { get; set; }
        public string? SACCode { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal TotalBillValue { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public virtual Company? Company { get; set; }
    }
}
