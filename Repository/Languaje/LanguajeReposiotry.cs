using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace cotr.backend.Repository.Languaje
{
    public class LanguajeReposiotry : ILanguajeRepository
    {
        private readonly CotrContext _context;

        public LanguajeReposiotry(CotrContext context)
        {
            _context = context;
        }

        public async Task<List<Languajes>> GetLanguajesAsync()
        {
            try
            {
                return await _context.Languajes.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<Languajes> GetLanguajeById(short languajeId)
        {
            try
            {
                return await _context.Languajes.FirstOrDefaultAsync(x => x.LanguajeId.Equals(languajeId)) ?? throw new ApiException(404, "No se ha encontrado ese lenguaje de programación");
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
