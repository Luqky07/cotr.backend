namespace cotr.backend.Model.Request
{
    public class AttemptRequest
    {
        public string Code { get; set; }

        public AttemptRequest(string code)
        {
            Code = code;
        }
    }
}
