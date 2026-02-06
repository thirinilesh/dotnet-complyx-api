using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Subcontractor
{
    public  class SubcontractorRequestModel
    {
        public int SubcontractorID { get; set; }
        public int? CompanyID { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string? Address { get; set; } = string.Empty;
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
