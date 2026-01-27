using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ComplyX_Businesss.Models
{
    public class PayrollData
    {
        [Key]
        public int PayrollID { get; set; }
        public int? EmployeeID { get; set; }
        public string Month {  get; set; }
        public decimal Basic {  get; set; }
        public decimal HRA { get; set; }        
        public decimal SpecialAllowance { get; set; }
        public decimal VariablePay { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal PF { get; set; }
        public decimal ESI { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal TDS { get; set; }
        public decimal NetPay { get; set; }
        public string BankAccount { get; set; }
        public string IFSC { get; set; }
        [JsonIgnore]
        public virtual Employees? Employees { get; set; }

    }
}
