using ComplyX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplyX.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Domain { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string ContactPhone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string GSTIN { get; set; } = string.Empty;

        public string PAN { get; set; } = string.Empty;

        public bool IsActive { get; set; }  

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int MaxEmployees { get; set; }
        public int? ProductOwnerId { get; set; }
        public virtual ProductOwners ProductOwners { get; set; }

        public virtual ICollection<Subcontractors>? Subcontractorss { get; set; } = new List<Subcontractors>();

    }
}
