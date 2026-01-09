using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class TDSReturn
    {
        public int ReturnID { get; set; }
        public int DeductorID { get; set; }
        public string FormType { get; set; }
        public string  FinancialYear {get; set;}
        public string Quarter { get; set; }
        public string ReturnType { get; set; }
        public string? OriginalTokenNo { get; set; }
        public string Status { get; set; }
        public string? FVUVersion { get; set; }
        public string? RPUVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual TDSDeductor? TDSDeductor { get; set; }
    }
}
