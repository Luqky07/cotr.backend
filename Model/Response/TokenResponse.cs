namespace cotr.backend.Model.Response
{
    public class TokenResponse
    {
        public string Token { get; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}
