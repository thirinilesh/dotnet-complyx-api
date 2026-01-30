using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Data.Entities
{
    public class ApplicationUsers : IdentityUser
    {
        internal object Address;

        public bool? IsApproved { get; set; } = true;
        public DateTime? ApprovedDeniedDate { get; set; }
        public string? ApprovedDeniedBy { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
       

    }
}
