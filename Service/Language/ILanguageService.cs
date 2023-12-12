using cotr.backend.Model.Tables;

namespace cotr.backend.Service.Language
{
    public interface ILanguageService
    {
        Task<List<Languages>> GetLanguagesAsync();
        Task<Languages> GetLanguageByIdAsync(short languageId);
    }
}
