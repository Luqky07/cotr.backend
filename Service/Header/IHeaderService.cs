namespace cotr.backend.Service.Header
{
    public interface IHeaderService
    {
        int GetTokenSubUserId(IHeaderDictionary headers);
    }
}
