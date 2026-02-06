using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Tdschallan
{
    public class TdschallanResponseModel
    {
        public int ChallanId { get; set; }

        public int DeductorId { get; set; }

        public string Bsrcode { get; set; } = null!;

        public DateOnly ChallanDate { get; set; }

        public string ChallanSerialNo { get; set; } = null!;

        public string SectionCode { get; set; } = null!;

        public decimal TaxAmount { get; set; }

        public decimal? InterestAmount { get; set; }

        public decimal? LateFeeAmount { get; set; }

        public decimal? OtherAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool MatchedWithOltas { get; set; }

        public bool IsMappedToReturn { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
