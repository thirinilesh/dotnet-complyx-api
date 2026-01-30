using ComplyX.Data.DbContexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;

public partial class AspNetRole : IdentityRole<string>
{
    [Key]
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();
    public virtual ICollection<AspNetUserRole> AspNetUserRole { get; set; } = new List<AspNetUserRole>();
    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
