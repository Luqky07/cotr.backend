namespace cotr.backend.Model.Request
{
    public class VerifyEmailRequest
    {
        public string Token { get; set; }

        public VerifyEmailRequest(string token)
        {
            Token = token;
        }
    }
}
