





namespace ComplyX_Tests.Service
{
    [Serializable]
    internal class ApiExceptionBadRequest : Exception
    {
        public ApiExceptionBadRequest()
        {
        }

        public ApiExceptionBadRequest(string? message) : base(message)
        {
        }

        public ApiExceptionBadRequest(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}