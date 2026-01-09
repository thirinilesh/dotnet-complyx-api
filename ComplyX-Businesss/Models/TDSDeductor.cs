using Nest;
using NHibernate.Linq.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class TDSDeductor
    {
        [Key]
        public int DeductorID { get; set; }
        public int CompanyID { get; set; }
        public string DeductorName {  get; set; }     
        public string? TAN { get; set; }    
        public string? PAN { get; set; }
        public string DeductorCategory { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string? Phone {  get; set; }
        public string? Email { get; set; }
        public string? AOCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get;set; }
        public string?  UpdatedBy { get; set; }
        public virtual Company? Company { get; set; }

        public virtual ICollection<TDSReturn>? TDSReturns { get; set; } = new List<TDSReturn>();
    }
}
