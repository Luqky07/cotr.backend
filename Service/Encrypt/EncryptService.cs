namespace cotr.backend.Service.Encrypt
{
    public class EncryptService : IEncryptService
    {
        public string EncryptPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(4);
        }

        public bool ValidatePassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
