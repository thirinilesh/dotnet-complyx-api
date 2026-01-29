using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class GstSale
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
