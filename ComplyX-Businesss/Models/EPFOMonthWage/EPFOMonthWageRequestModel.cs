using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.EPFOMonthWage
{
    public class EPFOMonthWageRequestModel
    {
        public int WageId { get; set; }

        public int EmployeeId { get; set; }

        public int CompanyId { get; set; }

        public int? SubcontractorId { get; set; }

        public string WageMonth { get; set; } = null!;

        public decimal Wages { get; set; }

        public decimal Epfwages { get; set; }

        public decimal Epswages { get; set; }

        public decimal Edliwages { get; set; }

        public decimal Contribution { get; set; }

        public decimal EmployerShare { get; set; }

        public decimal PensionShare { get; set; }

        public int? Ncpdays { get; set; }

        public decimal? RefundAdvance { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
