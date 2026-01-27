
using ComplyX_Businesss.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public int ProductOwnerId { get; set; }
        [JsonIgnore]
        public virtual ProductOwners? ProductOwners { get; set; }
        [JsonIgnore]
        public virtual ICollection<Subcontractors>? Subcontractorss { get; set; } = new List<Subcontractors>();
        [JsonIgnore]
        public virtual ICollection<Employees>? Employees { get; set; } = new List<Employees>();
        [JsonIgnore]
        public virtual ICollection<CompanyEPFO>? CompanyEPFO { get; set; } = new List<CompanyEPFO>();
        [JsonIgnore]
        public virtual ICollection<EPFOECRFile>? EPFOECRFile { get; set; } = new List<EPFOECRFile>();
        [JsonIgnore]
        public virtual ICollection<EPFOPeriod>? EPFOPeriods { get; set; } = new List<EPFOPeriod>();
        [JsonIgnore]
        public virtual ICollection<SubscriptionInvoices>? SubscriptionInvoices { get; set; } = new List<SubscriptionInvoices>();
        [JsonIgnore]
        public virtual ICollection<CustomerPayments>? CustomerPayments { get; set; } = new List<CustomerPayments>();
        [JsonIgnore]
        public virtual ICollection<Gratuity_Policy>? Gratuity_Policy { get; set; } = new List<Gratuity_Policy>();
        [JsonIgnore]
        public virtual ICollection<GST_HSN_Mapping>? GST_HSN_Mappings { get; set; } = new List<GST_HSN_Mapping>();
        [JsonIgnore]
        public virtual ICollection<GST_InvoiceSeries>? GST_InvoiceSeries { get; set; } = new List<GST_InvoiceSeries>();
        [JsonIgnore]
        public virtual ICollection<GST_Purchase>? GST_Purchases { get; set; } = new List<GST_Purchase>();
        [JsonIgnore]
        public virtual ICollection<GST_Returns>? GST_Returns { get; set; } = new List<GST_Returns>();
        [JsonIgnore]
        public virtual ICollection<GST_Sales>? GST_Sales { get; set; } = new List<GST_Sales>();
        [JsonIgnore]
        public virtual ICollection<Leave_Encashment_Policy>? Leave_Encashment_Policy { get; set; } = new List<Leave_Encashment_Policy>();
        [JsonIgnore]
        public virtual ICollection<FnF_Calculations>? FnF_Calculations { get; set; } = new List<FnF_Calculations>();
        [JsonIgnore]
        public virtual ICollection<Gratuity_Transactions>? Gratuity_Transactions { get; set; } = new List<Gratuity_Transactions>();
        [JsonIgnore]
        public virtual ICollection<EPFOMonthlyWage>? EPFOMonthlyWage { get; set; } = new List<EPFOMonthlyWage>();
        [JsonIgnore]
        public virtual ICollection<TDSDeductor>? TDSDeductor { get; set; } = new List<TDSDeductor>();
        [JsonIgnore]
        public virtual ICollection<TDSDeductee>? TDSDeductee { get; set; } = new List<TDSDeductee>();
        [JsonIgnore]

        public virtual ICollection<CompanyPartyRole>? CompanyPartyRole { get; set; } = new List<CompanyPartyRole>();
    }
}
