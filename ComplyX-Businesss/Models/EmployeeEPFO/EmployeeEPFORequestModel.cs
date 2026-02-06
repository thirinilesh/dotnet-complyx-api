using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.EmployeeEPFO
{
    public class EmployeeEPFORequestModel
    {
        public int EmployeeEpfoid { get; set; }

        public int EmployeeId { get; set; }

        public string Uan { get; set; } = null!;

        public string PfaccountNumber { get; set; } = null!;

        public DateOnly? DojEpf { get; set; }

        public DateOnly? DoeEpf { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
