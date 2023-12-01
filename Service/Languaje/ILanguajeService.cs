using cotr.backend.Model.Tables;

namespace cotr.backend.Service.Languaje
{
    public interface ILanguajeService
    {
        Task<List<Languajes>> GetLanguajesAsync();
    }
}
