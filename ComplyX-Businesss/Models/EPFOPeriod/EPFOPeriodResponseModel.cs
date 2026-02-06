using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.EPFOPeriod
{
    public class EPFOPeriodResponseModel
    {
        public int EpfoperiodId { get; set; }

        public int CompanyId { get; set; }

        public int? SubcontractorId { get; set; }

        public int PeriodYear { get; set; }

        public int PeriodMonth { get; set; }

        public string Status { get; set; } = null!;

        public string? EcrfilePath { get; set; }

        public string? Trrn { get; set; }

        public DateTime? Trrndate { get; set; }

        public string? ChallanFilePath { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedByUserId { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockedAt { get; set; }

        public string? LockedByUserId { get; set; }
    }
}
