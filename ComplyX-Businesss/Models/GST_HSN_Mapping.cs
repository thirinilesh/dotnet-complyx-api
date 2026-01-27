using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class GST_HSN_Mapping
    {
        public int MappingID { get; set; }
        public int CompanyID { get; set; }
        public int ItemID {  get; set; }
        public string? HSNCode { get; set; }
        public string? SACCode { get; set; }
        public string? GST_Rate { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
        [JsonIgnore]
        public virtual GST_HSNSAC? GST_HSNSAC { get; set; }
    }
}
