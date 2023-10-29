using cotr.backend.Model.Response;

namespace cotr.backend.Service.Token
{
    public interface ITokenService
    {
        string GetToken(bool isAccess);
    }
}
