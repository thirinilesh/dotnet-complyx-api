using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplyX.Data.Entities;

public partial class GST_Sales_Invoice
{
  //  public int SaleId { get; set; }

    public int CompanyId { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string InvoiceNo { get; set; } = null!;

    public DateOnly InvoiceDate { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerGstin { get; set; }

    public string? PlaceOfSupply { get; set; }

    public decimal TotalInvoiceValue { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<Gst_Sales_Items> Gst_Sales_Items { get; set; } = new List<Gst_Sales_Items>();
}
