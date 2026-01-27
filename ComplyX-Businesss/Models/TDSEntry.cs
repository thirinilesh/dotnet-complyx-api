using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class TDSEntry
    {
        [Key]
        public int EntryID { get; set; }
        public int DeducteeID { get; set; }
        public int DeductorID { get; set; }
        public string SectionCode { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; } = 0;
        public decimal TaxableAmount { get; set; } = 0;
        public decimal TDSRate { get; set; } = 0;
        public decimal TDSAmount { get; set; }= 0;
        public decimal? Surcharge { get; set; } = 0;
        public decimal? Cess { get; set; } = 0;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalTDS { get; set; } = 0;
        public bool HigherRateApplied { get; set; } 
        public string? HigherRateReason { get; set; }
        public bool IsMappedToReturn { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public virtual TDSDeductor? TDSDeductor { get; set; }
        [JsonIgnore]
        public virtual TDSDeductee? TDSDeductee { get; set; }
        [JsonIgnore]
        public virtual ICollection<TDSReturnEntry>? TDSReturnEntry { get; set; } = new List<TDSReturnEntry>();
        [JsonIgnore]
        public virtual ICollection<TDSChallanAllocation>? TDSChallanAllocation { get; set; } = new List<TDSChallanAllocation>();
    } 

}
