using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyX_Businesss.Helper.Commanfield;

namespace ComplyX_Businesss.Models
{
    public class TDSReturn
    {
        public int ReturnID { get; set; }
        public int DeductorID { get; set; }
        [Required]         
        public string FormType { get; set; }
        [Required]      
        public string  FinancialYear {get; set;}
        public string Quarter { get; set; }
        [Required]
        [EnumDataType(typeof(ReturnType),
        ErrorMessage = "Invalid ReturnType. Allowed values: REGULAR, CORRECTION")]
        public string ReturnType { get; set; }
        public string? OriginalTokenNo { get; set; }
        [Required]
        [EnumDataType(typeof(TdsReturnStatus),
        ErrorMessage = "Invalid Status. Allowed values: DRAFT, VALIDATED, FILED, REVISED, REJECTED")]
        public string Status { get; set; }
        public string? FVUVersion { get; set; }
        public string? RPUVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual TDSDeductor? TDSDeductor { get; set; }
        public virtual ICollection<TDSReturnChallan>? TDSReturnChallan { get; set; } = new List<TDSReturnChallan>();
        public virtual ICollection<TDSReturnEntry>? TDSReturnEntry { get; set; } = new List<TDSReturnEntry>();
    }
}
