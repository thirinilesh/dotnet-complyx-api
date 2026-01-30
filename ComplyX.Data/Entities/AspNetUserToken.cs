using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class AspNetUserToken :IdentityUserToken<string>
{
    public string UserId { get; set; } = null!;

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
