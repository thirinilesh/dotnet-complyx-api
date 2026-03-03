using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.CompanyBranches
{
    public  class CompanyBranchesResponseModel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? GSTIN { get; set; }
        public bool? IsHeadOffice { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
