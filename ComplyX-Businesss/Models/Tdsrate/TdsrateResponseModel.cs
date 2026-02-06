using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Tdsrate
{
    public class TdsrateResponseModel
    {
        public int TaxId { get; set; }

        public string TaxName { get; set; } = null!;

        public decimal Rate { get; set; }

        public string TaxType { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
