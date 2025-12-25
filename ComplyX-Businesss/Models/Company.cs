
using ComplyX_Businesss.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplyX_Businesss.Models
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
        public virtual ProductOwners? ProductOwners { get; set; }

        public virtual ICollection<Subcontractors>? Subcontractorss { get; set; } = new List<Subcontractors>();

        public virtual ICollection<Employees>? Employees { get; set; } = new List<Employees>();

        public virtual ICollection<CompanyEPFO>? CompanyEPFO { get; set; } = new List<CompanyEPFO>();

        public virtual ICollection<EPFOECRFile>? EPFOECRFile { get; set; } = new List<EPFOECRFile>();

        public virtual ICollection<EPFOPeriod>? EPFOPeriods { get; set; } = new List<EPFOPeriod>();
        public virtual ICollection<SubscriptionInvoices>? SubscriptionInvoices { get; set; } = new List<SubscriptionInvoices>();

        public virtual ICollection<CustomerPayments>? CustomerPayments { get; set; } = new List<CustomerPayments>();
        public virtual ICollection<Gratuity_Policy>? Gratuity_Policy { get; set; } = new List<Gratuity_Policy>();
    }
}
