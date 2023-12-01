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
    }
}
