using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class legalDocumentVersion
    {
        [Key]
        public int version_id { get; set; }
        public int document_id { get; set; }
        public string version_number { get; set; }
        public string version_type { get; set; }
        public string html_content { get; set; }
        public string content_hash { get; set; }
        public DateOnly effective_from_date { get; set; }
        public DateOnly release_date { get; set; }
        public DateOnly? expiry_date { get; set; }
        public string? change_summary { get; set; }
        public string? legal_basis { get; set; }

        public bool? is_published { get; set; }
        public bool? is_active { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public DateTime? approved_at { get; set; }
        public string? approved_by { get; set; }
        [JsonIgnore]
        public virtual legalDocument? legalDocument { get; set; }
        [JsonIgnore]
        public virtual ICollection<legalDocumentAcceptance>? legalDocumentAcceptance { get; set; } = new List<legalDocumentAcceptance>();
    }
}
