using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Tables;

namespace cotr.backend.Service.User
{
    public interface IUserService
    {
        Task<Users> ValidateUserAsync(LoginRequest request);
        Task<EmailMessage> SignupUserAsync(SignupRequest request);
        Task UpdatePasswordAsync(UpdatePasswordRequest updatePassword);
        Task<EmailMessage> RecoverPasswordAsync(string email);
        Task<Users> GetUserInfoByIdAsync(int userId);
        Task VerifyEmailAsync(VerifyEmailRequest request);
        Task VerifyRefreshToken(int userId);
    }
}
