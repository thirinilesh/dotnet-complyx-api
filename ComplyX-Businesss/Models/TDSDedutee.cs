using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyX_Businesss.Helper.Commanfield;

namespace ComplyX_Businesss.Models
{
    public  class TDSDeductee
    {
        [Key]
        public int DeducteeID { get; set; }
        public int CompanyID { get; set; }
        [Required]
        [EnumDataType(typeof(DeducteeType),
       ErrorMessage = "Invalid ReturnType. Allowed values:  EMPLOYEE,VENDOR,PENSIONER,OTHER")]
        public string DeducteeType { get; set; }
        public string DeducteeName { get; set; }
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN format.")]
        public string? PAN { get; set; }
        public string PANStatus { get; set; }
        public bool? AadhaarLinked { get; set; }
        public string ResidentStatus { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ICollection<TDSEntry>? TDSEntry { get; set; } = new List<TDSEntry>();

    }
}
