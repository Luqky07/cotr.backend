using cotr.backend.Model.DataModel;

namespace cotr.backend.Model.Response
{
    public class TryExceptionResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public TryExceptionResponse(string type, string message)
        {
            Type = type;
            Message = message;
        }

        public TryExceptionResponse(TryException ex)
        {
            Type = ex.Type;
            Message = ex.Message;
        }
    }
}
