using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplyX.Data.Entities;
 
public partial class RegisterUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserID { get; set; }
    public string? UserName { get; set; }

    public string Password { get; set; } = null!;

    public string? Domain { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? State { get; set; }

    public string? Gstin { get; set; }

    public string? Pan { get; set; }
}
