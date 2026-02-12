using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class CommonDropdownModel
    {
        public int id { get; set; }

        public string name { get; set; } = null!;
        public bool? isActive { get; set; }

    }
}
