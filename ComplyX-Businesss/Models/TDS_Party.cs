using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class TDS_Party
    {
        [Key]
        public int PartyID { get; set; }
        public string PartyType { get; set; }
        public int RefID { get; set; }
        public string PAN {  get; set; }
        public string? TAN { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
