using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using cotr.backend.Service.Token;
using cotr.backend.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cotr.backend.Controllers
{
    [ApiController]
    [Route("user")]
    #if DEBUG
        [AllowAnonymous]
    #else
        [Authorize]
    #endif
    public class UsersController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UsersController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateCredentialsAsync(LoginRequest request)
        {
            try
            {
                Users user = await _userService.ValidateUserAsync(request);

                return Ok(new LoginResponse(user, _tokenService.GetToken(true), _tokenService.GetToken(false)));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignupAsync(SignupRequest request)
        {
            try
            {
                await _userService.SignupUserAsync(request);

                return NoContent();
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}
