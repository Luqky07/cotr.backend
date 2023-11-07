using cotr.backend.Model.Tables;

namespace cotr.backend.Model.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public LoginResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
