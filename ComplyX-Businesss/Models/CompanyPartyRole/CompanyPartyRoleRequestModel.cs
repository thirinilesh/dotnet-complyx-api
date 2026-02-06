using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyX_Businesss.Helper.Commanfield;

namespace ComplyX_Businesss.Models.CompanyPartyRole
{
    public  class CompanyPartyRoleRequestModel
    {
        public int CompanyPartyRoleID { get; set; }
        public int CompanyID { get; set; }
        public int PartyID { get; set; }
        [Required]
        [EnumDataType(typeof(RoleType),
       ErrorMessage = "Invalid Status. Allowed values: SUPPLIER, CUSTOMER, SUBCONTRACTOR, DEDUCTEE, TRANSPORTER")]
        public string RoleType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
