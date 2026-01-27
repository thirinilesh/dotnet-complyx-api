using ComplyX.Shared.Data;
using System.Text.Json.Serialization;

namespace ComplyX_Businesss.Models
{
  

    public class CompanyEPFO
    {
        public int CompanyEPFOId { get; set; }
        public int CompanyId { get; set; }
        public string EstablishmentCode { get; set; }
        public string? Extension { get; set; }
        public string? OfficeCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public virtual Company Companies { get; set; }
    }

    public class EmployeeEPFO
    {
        public int EmployeeEPFOId { get; set; }
        public int EmployeeID { get; set; }
        public string UAN { get;  set; }
        public string PFAccountNumber { get; set; }
        public DateTime? DOJ_EPF { get; set; }
        public DateTime? DOE_EPF { get; set; }
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public virtual Employees Employees { get; set; }
    }

    public class EPFOECRFile
    {
        public int ECRFileId { get;set; }
        public int CompanyId { get; set; }
        public int? SubcontractorId { get; set; }
        public string WageMonth { get; set; }
        public string FileName { get;set; }
        public int TotalEmployees { get; set; }
        public decimal TotalWages { get; set; }
        public decimal TotalContribution { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public virtual Company? Companies { get; set; }
        [JsonIgnore]
        public virtual Subcontractors? Subcontractorss { get; set; }
    }

    public class EPFOPeriod
    {
        public int EPFOPeriodId { get; set; }
        public int CompanyID { get; set; }
        public int? SubcontractorId { get; set; }
        public int PeriodYear { get; set; }
        public int  PeriodMonth { get; set; }
        public string Status { get; set; }
        public string? ECRFilePath { get; set; }
        public string? TRRN { get; set; }
        public DateTime? TRRNDate { get; set; }
        public string? ChallanFilePath { get; set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public bool IsLocked { get; set; } = false;
        public DateTime? LockedAt { get; set; }
        public string? LockedByUserId { get; set; }
        [JsonIgnore]
        public virtual Company Companies { get; set; }
        [JsonIgnore]
        public virtual Subcontractors? Subcontractorss { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser? CreatedByUser { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser? LockedByUser { get; set; }
    }
}
