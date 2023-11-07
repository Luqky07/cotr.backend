namespace cotr.backend.Service.Token
{
    public interface ITokenService
    {
        string GetToken(int userId, bool isAccess);
    }
}
