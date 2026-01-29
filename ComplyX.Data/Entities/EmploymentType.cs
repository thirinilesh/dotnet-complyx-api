using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class EmploymentType
{
    public int EmploymentTypeId { get; set; }

    public string Name { get; set; } = null!;
}
