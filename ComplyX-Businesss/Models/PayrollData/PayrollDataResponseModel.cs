using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.PayrollData
{
    public class PayrollDataResponseModel
    {
        public int PayrollId { get; set; }

        public int? EmployeeId { get; set; }

        public string? Month { get; set; }

        public decimal? Basic { get; set; }

        public decimal? Hra { get; set; }

        public decimal? SpecialAllowance { get; set; }

        public decimal? VariablePay { get; set; }

        public decimal? GrossSalary { get; set; }

        public decimal? Pf { get; set; }

        public decimal? Esi { get; set; }

        public decimal? ProfessionalTax { get; set; }

        public decimal? Tds { get; set; }

        public decimal? NetPay { get; set; }

        public string? BankAccount { get; set; }

        public string? Ifsc { get; set; }

    }
}
