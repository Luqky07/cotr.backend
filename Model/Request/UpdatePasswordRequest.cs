namespace cotr.backend.Model.Request
{
    public class UpdatePasswordRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }

        public UpdatePasswordRequest(string token, string password)
        {
            Token = token;
            Password = password;
        }
    }
}
