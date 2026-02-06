using ComplyX_Businesss.Models;
using Microsoft.AspNetCore.Identity;

namespace ComplyX.Shared.Data
{
    public class ApplicationUser : IdentityUser
    {
        internal object Address;

        public bool? IsApproved { get; set; } = true;
        public DateTime? ApprovedDeniedDate { get; set; }
        public string? ApprovedDeniedBy { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
       
       
    }
}
