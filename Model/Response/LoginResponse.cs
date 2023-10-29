using cotr.backend.Model.Tables;

namespace cotr.backend.Model.Response
{
    public class LoginResponse
    {
        public Users User {  get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public LoginResponse(Users user, string accessToken, string refreshToken)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
