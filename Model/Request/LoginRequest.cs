namespace cotr.backend.Model.Request
{
    public class LoginRequest
    {
        public string User { get; }
        public string Password { get; }

        public LoginRequest(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}
