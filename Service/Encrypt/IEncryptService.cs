namespace cotr.backend.Service.Encrypt
{
    public interface IEncryptService
    {
        string EncryptPassword(string password, string salt);
        string GenerateSalt();
        bool ValidatePassword(string password, string hashedPassword);
    }
}
