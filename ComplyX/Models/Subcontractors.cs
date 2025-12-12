using System.Net;
using System;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Models
{
    public class Subcontractors
    {
        [Key]
        public int SubcontractorID { get; set; }
        public int? CompanyID { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string? Address { get; set; } = string.Empty;
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Company Companies { get; set; }

    }
}
