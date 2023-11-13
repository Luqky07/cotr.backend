namespace cotr.backend.Model.Request
{
    public class EmailUpdatePasswordRequest
    {
        public string Email { get; set; }

        public EmailUpdatePasswordRequest(string email)
        {
            Email = email;
        }
    }
}
