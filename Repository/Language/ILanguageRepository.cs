using cotr.backend.Model.Tables;

namespace cotr.backend.Repository.Language
{
    public interface ILanguageRepository
    {
        Task<List<Languages>> GetLanguagesAsync();
        Task<Languages> GetLanguageById(short languageId);
    }
}
