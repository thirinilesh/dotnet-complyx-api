using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

[Keyless]
public partial class RegisterUser
{
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
