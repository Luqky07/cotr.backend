using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using cotr.backend.Service.Email;
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
        private readonly IEmailService _emailService;

        public UsersController(ITokenService tokenService, IUserService userService, IHeaderService headerService, IEmailService emailService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _headerService = headerService;
            _emailService = emailService;
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
                EmailMessage message = await _userService.SignupUserAsync(request);

                await _emailService.SendEmailAsync(message);

                return NoContent();
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpPatch("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordAsync(UpdatePasswordRequest request)
        {
            try
            {
                await _userService.UpdatePasswordAsync(request);

                return NoContent();
            }
            catch(ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpPost("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordRequestAsync(EmailUpdatePasswordRequest request)
        {
            try
            {
                EmailMessage message = await _userService.RecoverPasswordAsync(request.Email);

                await _emailService.SendEmailAsync(message);

                return NoContent();
            }
            catch(ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
            
        [HttpGet("access-token")]
        [Authorize(AuthenticationSchemes = "Refresh")]
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

        [HttpGet("profile/{userIdWanted}")]
        [Authorize(AuthenticationSchemes = "Access")]
        public async Task<IActionResult> GetProfileInfoByIdAsync(int userIdWanted)
        {
            try
            {
                return Ok(await _userService.GetUserInfoByIdAsync(userIdWanted));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpPatch("verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmailAsync(VerifyEmailRequest request)
        {
            try
            {
                await _userService.VerifyEmailAsync(request);

                return NoContent();
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}
