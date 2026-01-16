using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class legalDocument
    {
        [Key]
        public int document_id { get; set; }
        public string document_name { get; set; }
        public string document_code { get; set; }
        public string? applicable_region { get; set; }
        public string? status { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

    }
}
