namespace cotr.backend.Model.Response
{
    public class ApiExceptionResponse
    {
        public short StatusCode { get; }
        public string Message { get; }

        public ApiExceptionResponse(short statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ApiExceptionResponse(ApiException ex)
        {
            StatusCode = ex.StatusCode;
            Message = ex.Message;
        }
    }
}
