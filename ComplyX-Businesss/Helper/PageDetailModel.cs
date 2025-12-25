namespace ComplyX.Shared.Helper
{
    public class PageDetailModel
    {
        public string  result {get; set;}
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Count { get; set; }
        public string? SearchText { get; set; }
        public IList<string>? FilterdCount { get; set; }
    }

}
