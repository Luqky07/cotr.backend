using cotr.backend.Model;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.Language;

namespace cotr.backend.Service.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<List<Languages>> GetLanguagesAsync()
        {
            return await _languageRepository.GetLanguagesAsync();
        }

        public async Task<Languages> GetLanguageByIdAsync(short languageId)
        {
            List<Languages> languajes = await _languageRepository.GetLanguagesAsync();
            return languajes.FirstOrDefault(x => x.LanguageId.Equals(languageId)) ?? throw new ApiException(404, "No disponemos de información para ese lenguaje");
        }
    }
}
