using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class TDSChallanAllocation
    {
        public int AllocationID { get; set; }
        public int ChallanID { get; set; }
        public int EntryID { get; set; }
        public decimal AllocatedTDSAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public virtual TDSChallan? TDSChallan { get; set; }
        public virtual TDSEntry? TDSEntry { get; set; }
    }
}
