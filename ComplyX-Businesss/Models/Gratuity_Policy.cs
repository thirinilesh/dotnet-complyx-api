using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class Gratuity_Policy
    {
        public  int  PolicyID { get; set;}
        public int CompanyID { get; set;}
        public int MinimumServiceYears { get; set;}
        public string? Formula { get; set;}
        public decimal? MaxGratuityAmount { get; set;}
        public bool Eligible { get; set;}
        public DateTime? CreatedAt { get; set;}
        public DateTime?  UpdatedAt { get; set;}
        public virtual Company? Company { get; set; }
    }
}
