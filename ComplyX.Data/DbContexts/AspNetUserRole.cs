using ComplyX.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ComplyX.Data.DbContexts
{
    public class AspNetUserRole : IdentityUserRole<string>
    {
        
        
        public virtual AspNetUser User { get; set; } = null!;
        public virtual  AspNetRole Role { get; set; } = null!;
    }
}