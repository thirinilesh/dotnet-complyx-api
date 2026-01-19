using System.ComponentModel.DataAnnotations;
 
namespace ComplyX_Businesss.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeID {  get; set; }
        public int CompanyID { get; set; }
        public int? SubcontractorID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string FatherSpouseName { get; set; } = string.Empty;
        public DateTime DOB {  get; set; }
        public string Gender { get; set; } = string.Empty;
        public string MaritalStatus {  get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string PAN {  get; set; } = string.Empty;
        public string Aadhaar { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PresentAddress { get; set; } = string.Empty;
        public string PermanentAddress {  get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;
        public DateTime DOJ {  get; set; }
        public DateTime ConfirmationDate {  get; set; } = DateTime.Now;
        public string Designation {  get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Grade {  get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string WorkLocation { get; set; } = string.Empty;
        public int ReportingManager { get; set; } 
        public DateTime? ExitDate {  get; set; }  
        public string? ExitType { get; set; } = string.Empty;
        public string? ExitReason { get; set; } = string.Empty;
        public string UAN { get; set; } = string.Empty;
        public string PFAccountNumber { get; set; } = string.Empty;
        public string ESIC_IP { get; set; } = string.Empty;
        public string PTState { get; set; } = string.Empty;
        public bool ActiveStatus { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public virtual Company Companies { get; set; }
        public virtual Subcontractors? Subcontractor { get; set; }

        public virtual ICollection<PayrollData> Payroll { get; set; } = new List<PayrollData>();
        public virtual ICollection<EmployeeEPFO>? EmployeeEPFO { get; set; } = new List<EmployeeEPFO>();
        public virtual ICollection<Gratuity_Transactions>? Gratuity_Transactions { get; set; } = new List<Gratuity_Transactions>();
        public virtual ICollection<EPFOMonthlyWage>? EPFOMonthlyWage { get; set; } = new List<EPFOMonthlyWage>();
       
    }
}
