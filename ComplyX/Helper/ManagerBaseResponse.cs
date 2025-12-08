namespace ComplyX.Helper
{
    public class ManagerBaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; } = 200;
        public T Result { get; set; }
        public PageDetailModel PageDetail { get; set; }
        public ManagerBaseResponse()
        {
            IsSuccess = true;
        }
    }
}
