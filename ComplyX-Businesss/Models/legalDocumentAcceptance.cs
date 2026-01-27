using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class legalDocumentAcceptance
    {
        [Key]
        public int acceptance_id {  get; set; }
        public string user_id { get; set; }
        public int document_id { get; set; }
        public int version_id { get; set; }
        public DateTime accepted_at { get; set; }
        public string? accepted_ip { get; set; }
        public string? accepted_device { get; set; }
        public string? acceptance_method { get; set; }
        public string consent_proof_hash { get; set; }
        [JsonIgnore]
        public virtual legalDocument? legalDocument { get; set; }
        [JsonIgnore]
        public virtual legalDocumentVersion? legalDocumentVersion { get; set; }

    }
}
