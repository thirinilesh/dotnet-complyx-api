using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Tdsreturn
{
    public  class TdsreturnResponseModel
    {
        public int ReturnId { get; set; }

        public int DeductorId { get; set; }

        public string FormType { get; set; } = null!;

        public string FinancialYear { get; set; } = null!;

        public string Quarter { get; set; } = null!;

        public string ReturnType { get; set; } = null!;

        public string? OriginalTokenNo { get; set; }

        public string Status { get; set; } = null!;

        public string? Fvuversion { get; set; }

        public string? Rpuversion { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
