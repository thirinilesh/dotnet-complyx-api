using Nest;
using NHibernate.Linq.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static ComplyX_Businesss.Helper.Commanfield;

namespace ComplyX_Businesss.Models
{
    public class TDSDeductor
    {
        [Key]
        public int DeductorID { get; set; }
        public int CompanyID { get; set; }
        public string DeductorName {  get; set; }     
        public string? TAN { get; set; }    
        public string? PAN { get; set; }
        [Required]
        [EnumDataType(typeof(DeductorCategory),
        ErrorMessage = "Invalid ReturnType. Allowed values: COMPANY,FIRM_LLP,INDIVIDUAL,HUF,GOVERNMENT,PSU_AUTONOMOUS,TRUST_NGO,BANK_FI,COOPERATIVE,FOREIGN_ENTITY")]
        public string DeductorCategory { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string? Phone {  get; set; }
        public string? Email { get; set; }
        public string? AOCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get;set; }
        public string?  UpdatedBy { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
        [JsonIgnore]
        public virtual ICollection<TDSReturn>? TDSReturns { get; set; } = new List<TDSReturn>();
        [JsonIgnore]
        public virtual ICollection<TDSEntry>? TDSEntry { get; set; } = new List<TDSEntry>();
        [JsonIgnore]
        public virtual ICollection<TDSChallan>? TDSChallan { get; set; } = new List<TDSChallan>();
    }
}
