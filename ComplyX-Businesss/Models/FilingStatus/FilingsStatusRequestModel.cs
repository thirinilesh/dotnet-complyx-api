using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.FilingStatus
{
    public class FilingsStatusRequestModel
    {
        public int FilingStatusId { get; set; }

        public string Name { get; set; } = null!;
    }
}
