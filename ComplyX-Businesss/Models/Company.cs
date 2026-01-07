
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
        public virtual ICollection<GST_HSN_Mapping>? GST_HSN_Mappings { get; set; } = new List<GST_HSN_Mapping>();
        public virtual ICollection<GST_InvoiceSeries>? GST_InvoiceSeries { get; set; } = new List<GST_InvoiceSeries>();
        public virtual ICollection<GST_Purchase>? GST_Purchases { get; set; } = new List<GST_Purchase>();
        public virtual ICollection<GST_Returns>? GST_Returns { get; set; } = new List<GST_Returns>();
        public virtual ICollection<GST_Sales>? GST_Sales { get; set; } = new List<GST_Sales>();
        public virtual ICollection<Leave_Encashment_Policy>? Leave_Encashment_Policy { get; set; } = new List<Leave_Encashment_Policy>();
        public virtual ICollection<FnF_Calculations>? FnF_Calculations { get; set; } = new List<FnF_Calculations>();
        public virtual ICollection<Gratuity_Transactions>? Gratuity_Transactions { get; set; } = new List<Gratuity_Transactions>();
        public virtual ICollection<EPFOMonthlyWage>? EPFOMonthlyWage { get; set; } = new List<EPFOMonthlyWage>();
    }
}
