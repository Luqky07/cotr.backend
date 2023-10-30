using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using cotr.backend.Service.Token;
using cotr.backend.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace cotr.backend.Controllers
{
    [ApiController]
    [Route("user")]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        #if DEBUG
            [AllowAnonymous]
        #else
            [Authorize(AuthenticationSchemes = "Refresh")]
        #endif
        [HttpGet("access-token")]
        public IActionResult AccessToken()
        {
            try
            {
                HttpContext.Request.Headers.TryGetValue("Authorization", out var value);
                if (value.IsNullOrEmpty()) throw new ApiException(400, "No se ha encontrado información del header");
                return Ok(new TokenResponse(_tokenService.GetToken(true)));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}
