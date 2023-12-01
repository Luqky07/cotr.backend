using cotr.backend.Model.Tables;

namespace cotr.backend.Repository.Languaje
{
    public interface ILanguajeRepository
    {
        Task<List<Languajes>> GetLanguajesAsync();
        Task<Languajes> GetLanguajeById(short languajeId);
    }
}
