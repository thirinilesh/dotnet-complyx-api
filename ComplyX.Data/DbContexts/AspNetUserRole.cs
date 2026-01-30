using ComplyX.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ComplyX.Data.DbContexts
{
    public class AspNetUserRole : IdentityUserRole<string>
    {
        public string UserID { get; set; } = null!;
        public string RoleID { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
        public virtual  AspNetRole Role { get; set; } = null!;
    }
}