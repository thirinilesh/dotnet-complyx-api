using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class TDSChallan
    {
        [Key]
        public int ChallanID { get; set; }
        public int DeductorID { get; set; }
        public string BSRCode { get; set; }
        public DateTime ChallanDate { get; set; }
        public string ChallanSerialNo { get; set; }
        public  string SectionCode { get; set; }
        public decimal TaxAmount { get; set; }  = 0m;
        public decimal? InterestAmount { get; set; } = 0m;
        public decimal? LateFeeAmount { get; set; } = 0m;
        public decimal? OtherAmount { get; set; } = 0m;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? TotalAmount { get; set; } = 0m;
        public bool MatchedWithOLTAS { get; set; }
        public bool IsMappedToReturn { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual TDSDeductor? TDSDeductor { get; set; }
    }
}
