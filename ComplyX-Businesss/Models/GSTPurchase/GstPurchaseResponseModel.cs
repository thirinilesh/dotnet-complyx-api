using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTPurchase
{
    public class GstPurchaseResponseModel
    {
        public int PurchaseId { get; set; }

        public int CompanyId { get; set; }

        public string BillNo { get; set; } = null!;

        public DateOnly BillDate { get; set; }

        public string? SupplierName { get; set; }

        public string? SupplierGstin { get; set; }

        public string? PlaceOfSupply { get; set; }

        public string? Hsncode { get; set; }

        public string? Saccode { get; set; }

        public decimal TaxableValue { get; set; }

        public decimal? Cgst { get; set; }

        public decimal? Sgst { get; set; }

        public decimal? Igst { get; set; }

        public decimal TotalBillValue { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }
    }
}
