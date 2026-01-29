namespace ComplyX.Shared.Helper
{
    public class ApiBaseResponse<T>
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public int? StatusCode { get; set; }
        public T? Result { get; set; }
    }
    public class ApiBasePageResponse<T>
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public int? StatusCode { get; set; }
        public T Result { get; set; }
        public PageDetailModel? PageDetail { get; set; }
    }

        public class ApiBaseFailResponse<T>
        {
            public string? Message { get; set; }
            public bool IsSuccess { get; set; } = false;
            public int? StatusCode { get; set; }  // Made nullable
            public T Result { get; set; }
        }
    
}
