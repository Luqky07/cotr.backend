using cotr.backend.Model;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.Languaje;

namespace cotr.backend.Service.Languaje
{
    public class LanguajeService : ILanguajeService
    {
        private readonly ILanguajeRepository _languajeRepository;

        public LanguajeService(ILanguajeRepository languajeRepository)
        {
            _languajeRepository = languajeRepository;
        }

        public async Task<List<Languajes>> GetLanguajesAsync()
        {
            return await _languajeRepository.GetLanguajesAsync();
        }

        public async Task<Languajes> GetLanguajeByIdAsync(short languajeId)
        {
            List<Languajes> languajes = await _languajeRepository.GetLanguajesAsync();
            return languajes.FirstOrDefault(x => x.LanguajeId.Equals(languajeId)) ?? throw new ApiException(404, "No disponemos de información para ese lenguaje");
        }
    }
}
