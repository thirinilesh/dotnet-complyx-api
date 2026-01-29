using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class EmployeeEpfo
{
    public int EmployeeEpfoid { get; set; }

    public int EmployeeId { get; set; }

    public string Uan { get; set; } = null!;

    public string PfaccountNumber { get; set; } = null!;

    public DateOnly? DojEpf { get; set; }

    public DateOnly? DoeEpf { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
