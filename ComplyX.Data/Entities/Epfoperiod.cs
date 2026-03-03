using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace ComplyX.Data.Entities;

public partial class Epfoperiod
{
    public int EpfoperiodId { get; set; }

    public int EPFOEstablishmentId { get; set; }
  
    public int PeriodYear { get; set; }

    public int PeriodMonth { get; set; }

    public string Status { get; set; } = null!;

    public string? EcrfilePath { get; set; }

    public string? Trrn { get; set; }

    public DateTime? Trrndate { get; set; }

    public string? ChallanFilePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedByUserId { get; set; }

    public bool IsLocked { get; set; }

    public DateTime? LockedAt { get; set; }
 
    public string? LockedByUserId { get; set; }

    public virtual EPFOEstablishment EPFOEstablishment { get; set; } = null!;
   
    public virtual AspNetUser? CreatedByUser { get; set; }
 
    public virtual AspNetUser? LockedByUser { get; set; }

}
