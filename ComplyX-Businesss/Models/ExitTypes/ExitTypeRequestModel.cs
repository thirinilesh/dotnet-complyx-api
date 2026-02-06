using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.ExitTypes
{
    public class ExitTypeRequestModel
    {
        public int ExitTypeId { get; set; }

        public string Name { get; set; } = null!;
    }
}
