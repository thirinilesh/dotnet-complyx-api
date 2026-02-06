using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTHSNSAC
{
    public class GSTHSNSACRequestModel
    {
        public int CodeId { get; set; }

        public string CodeType { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public decimal GstRate { get; set; }
    }
}
