using NHibernate.Collection.Generic;
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
    public  class PartyMaster
    {
        [Key]
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public string? PAN {  get; set; }
        public string? GSTIN { get; set; }
        [Required]
        [EnumDataType(typeof(PartyType),
        ErrorMessage = "Invalid Status. Allowed values: BUSINESS ,INDIVIDUAL ,GOVT ,UNREGISTERED")]
        public string PartyType { get; set; }
        public string Address1 {  get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string PinCode { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyPartyRole>? CompanyPartyRole { get; set; } = new List<CompanyPartyRole>();
    }
}
