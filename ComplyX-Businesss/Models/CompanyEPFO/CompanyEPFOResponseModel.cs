using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.CompanyEPFO
{
    public class CompanyEPFOResponseModel
    {
        public int CompanyEpfoid { get; set; }

        public int CompanyId { get; set; }

        public string EstablishmentCode { get; set; } = null!;

        public string? Extension { get; set; }

        public string? OfficeCode { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
