using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace cotr.backend.Repository.Language
{
    public class LanguageReposiotry : ILanguageRepository
    {
        private readonly CotrContext _context;

        public LanguageReposiotry(CotrContext context)
        {
            _context = context;
        }

        public async Task<List<Languages>> GetLanguagesAsync()
        {
            try
            {
                return await _context.Languages.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<Languages> GetLanguageById(short languajeId)
        {
            try
            {
                return await _context.Languages.FirstOrDefaultAsync(x => x.LanguageId.Equals(languajeId)) ?? throw new ApiException(404, "No se ha encontrado ese lenguaje de programación");
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
