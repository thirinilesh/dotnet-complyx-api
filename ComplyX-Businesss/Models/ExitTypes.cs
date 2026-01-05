using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class ExitTypes
    {
        [Key]
        public int ExitTypeID { get; set; }
        public string Name { get; set; }
    }
}
