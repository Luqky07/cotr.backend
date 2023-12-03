using cotr.backend.Model;
using cotr.backend.Model.Response;
using cotr.backend.Service.Languaje;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cotr.backend.Controllers
{
    [ApiController]
    [Route("languajes")]
    [Authorize(AuthenticationSchemes = "Access")]
    public class LanguajeController : Controller
    {
        private readonly ILanguajeService _languajeService;

        public LanguajeController(ILanguajeService languajeService)
        {
            _languajeService = languajeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguajesAsync()
        {
            try
            {
                return Ok(await _languajeService.GetLanguajesAsync());
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpGet("{languajeId}")]
        public async Task<IActionResult> GetLanguajeByIdAsync(short languajeId)
        {
            try
            {
                return Ok(await _languajeService.GetLanguajeByIdAsync(languajeId));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}
