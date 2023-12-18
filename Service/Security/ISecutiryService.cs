namespace cotr.backend.Service.Encrypt
{
    public interface ISecutiryService
    {
        string EncryptPassword(string password);
        bool ValidatePassword(string password, string hashedPassword);
        string RandomToken();
    }
}
