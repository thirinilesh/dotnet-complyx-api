using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GSTHSNMapping
{
    public class GSTHSNMappingResponseModel
    {
        public int MappingId { get; set; }

        public int CompanyId { get; set; }

        public int ItemId { get; set; }

        public string? Hsncode { get; set; }

        public string? Saccode { get; set; }

        public decimal? GstRate { get; set; }
    }
}
