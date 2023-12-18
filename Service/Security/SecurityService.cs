namespace cotr.backend.Service.Encrypt
{
    public class SecurityService : ISecutiryService
    {
        public string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(4));
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string RandomToken()
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new();

            return new(Enumerable.Repeat(CHARS, 15)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
    }
}
