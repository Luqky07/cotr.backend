namespace cotr.backend.Service.Encrypt
{
    public interface ISecutiryService
    {
        string EncryptPassword(string password, string salt);
        string GenerateSalt();
        bool ValidatePassword(string password, string hashedPassword);
        string RandomToken();
    }
}
