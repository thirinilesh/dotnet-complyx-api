using ComplyX.Data.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;
         
public partial class AspNetUser  : IdentityUser
{
    [Key]
    public string Id { get; set; } = null!;

    public bool? IsApproved { get; set; }

    public DateTime? ApprovedDeniedDate { get; set; }

    public string? ApprovedDeniedBy { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastPasswordChangeDate { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Epfoperiod> EpfoperiodCreatedByUsers { get; set; } = new List<Epfoperiod>();

    public virtual ICollection<Epfoperiod> EpfoperiodLockedByUsers { get; set; } = new List<Epfoperiod>();
    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
    public virtual ICollection<AspNetUserRole> AspNetUserRole { get; set; } = new List<AspNetUserRole>();
}
