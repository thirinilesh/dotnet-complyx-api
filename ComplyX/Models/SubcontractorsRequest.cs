namespace ComplyX.Models
{
    public class SubcontractorsRequest
    {
        public int SubcontractorID { get; set; }
        public int? CompanyID { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CompanyName { get; set; }
        public string Domain { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }

        public int? ProductOwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string OWnerEmail { get; set; }
        public string OrganizationName { get; set; }

    }
}
