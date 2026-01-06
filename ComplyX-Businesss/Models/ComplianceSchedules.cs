using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class ComplianceSchedules
    {
        [Key]
        public int ScheduleID { get; set; }
        public int CompanyID { get; set; }
        public string ComplianceType { get; set; }
        public string Frequency {  get; set; }
        public string? StateCode { get; set; }
        public int? BaseDay { get; set; }
        public int? QuarterMonth { get; set; }
        public int? OffsetDays { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
