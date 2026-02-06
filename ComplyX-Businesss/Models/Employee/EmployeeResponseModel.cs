using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Employee
{
    public class EmployeeResponseModel
    {
        public int EmployeeId { get; set; }

        public int? CompanyId { get; set; }

        public int? SubcontractorId { get; set; }

        public string? EmployeeCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FatherSpouseName { get; set; }

        public DateOnly? Dob { get; set; }

        public string? Gender { get; set; }

        public string? MaritalStatus { get; set; }

        public string? Nationality { get; set; }

        public string? Pan { get; set; }

        public string? Aadhaar { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? PresentAddress { get; set; }

        public string? PermanentAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Pincode { get; set; }

        public DateOnly? Doj { get; set; }

        public DateOnly? ConfirmationDate { get; set; }

        public string? Designation { get; set; }

        public string? Department { get; set; }

        public string? Grade { get; set; }

        public string? EmploymentType { get; set; }

        public string? WorkLocation { get; set; }

        public int? ReportingManager { get; set; }

        public DateOnly? ExitDate { get; set; }

        public string? ExitType { get; set; }

        public string? ExitReason { get; set; }

        public string? Uan { get; set; }

        public string? PfaccountNumber { get; set; }

        public string? EsicIp { get; set; }

        public string? Ptstate { get; set; }

        public bool? ActiveStatus { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
