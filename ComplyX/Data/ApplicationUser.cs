using ComplyX.Models;
using Microsoft.AspNetCore.Identity;

namespace ComplyX.Data
{
    public class ApplicationUser : IdentityUser
    {
       
        public bool? IsApproved { get; set; } = true;
        public DateTime? ApprovedDeniedDate { get; set; }
        public string? ApprovedDeniedBy { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
    }
}
