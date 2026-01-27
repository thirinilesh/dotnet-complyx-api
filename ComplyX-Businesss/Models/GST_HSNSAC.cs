using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class GST_HSNSAC
    {
        [Key]
        public int CodeID { get; set; }
        public string CodeType { get; set; }
        public string Code {  get; set; }
        public string? Description { get; set; }
        public decimal GST_Rate { get; set; }
        [JsonIgnore]
        public virtual ICollection<GST_HSN_Mapping>? GST_HSN_Mappings { get; set; } = new List<GST_HSN_Mapping>();

    }
}
