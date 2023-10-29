namespace cotr.backend.Service.Token
{
    public interface ITokenService
    {
        string GetToken(bool isAccess);
    }
}
