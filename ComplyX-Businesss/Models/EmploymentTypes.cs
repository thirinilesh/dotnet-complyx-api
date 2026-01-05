using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class EmploymentTypes
    {
        [Key]
        public int EmploymentTypeID { get; set; }
        public string Name { get; set; }
    }
}
