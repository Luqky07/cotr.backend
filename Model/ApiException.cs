namespace cotr.backend.Model
{
    public class ApiException: Exception
    {
        public short StatusCode { get; }

        public ApiException(short statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
