using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.TdschallanAllocation
{
    public class TdschallanAllocationResponseModel
    {
        public int AllocationId { get; set; }

        public int ChallanId { get; set; }

        public int EntryId { get; set; }

        public decimal AllocatedTdsamount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}
