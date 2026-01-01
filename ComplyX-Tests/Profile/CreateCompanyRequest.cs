namespace ComplyX_Tests.Profile
{
    public class CreateCompanyRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State {  get; set; }
        public string Zip {  get; set; }
    }
    public class Companies
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class CreateCompanyResponse
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}