using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using cotr.backend.Service.Header;
using cotr.backend.Service.Token;
using cotr.backend.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cotr.backend.Controllers
{
    [ApiController]
    [Route("user")]
    public class UsersController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IHeaderService _headerService;

        public UsersController(ITokenService tokenService, IUserService userService, IHeaderService headerService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _headerService = headerService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateCredentialsAsync(LoginRequest request)
        {
            try
            {
                Users user = await _userService.ValidateUserAsync(request);

                return Ok(new LoginResponse(_tokenService.GetToken(user.UserId, true), _tokenService.GetToken(user.UserId, false)));
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
                int userId = _headerService.GetTokenSubUserId(HttpContext.Request.Headers);

                return Ok(new TokenResponse(_tokenService.GetToken(userId, true)));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}
