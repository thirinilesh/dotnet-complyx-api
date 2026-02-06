using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.EPFOECRFile
{
    public class EPFOECRFileResponseModel
    {
        public int EcrfileId { get; set; }

        public int CompanyId { get; set; }

        public int? SubcontractorId { get; set; }

        public string WageMonth { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public int TotalEmployees { get; set; }

        public decimal TotalWages { get; set; }

        public decimal TotalContribution { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
