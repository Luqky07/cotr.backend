using cotr.backend.Model;
using cotr.backend.Model.Response;
using cotr.backend.Service.Language;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cotr.backend.Controllers
{
    [ApiController]
    [Route("languages")]
    [Authorize(AuthenticationSchemes = "Access")]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            try
            {
                return Ok(await _languageService.GetLanguagesAsync());
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetLanguageByIdAsync(short languageId)
        {
            try
            {
                return Ok(await _languageService.GetLanguageByIdAsync(languageId));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}
