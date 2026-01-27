using Microsoft.Graph.Places.Item.Descendants;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class EPFOMonthlyWage
    {
        [Key]
        public int WageId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int? SubcontractorId { get; set; }
        public string WageMonth {  get; set; }
        public decimal Wages { get; set; }
        public decimal EPFWages { get; set; }
        public decimal EPSWages { get; set; }
        public decimal EDLIWages { get; set; }
        public decimal Contribution {  get; set; }
        public decimal EmployerShare { get; set; }
        public decimal PensionShare { get; set; }
        public int? NCPDays { get; set; }
        public decimal? RefundAdvance { get; set; }
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
        [JsonIgnore]
        public virtual Employees? Employees { get; set; }
        [JsonIgnore]
        public virtual Subcontractors? Subcontractors { get; set; }

    }
}
