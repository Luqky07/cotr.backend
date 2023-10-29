using cotr.backend.Model.Request;
using cotr.backend.Model.Tables;

namespace cotr.backend.Service.User
{
    public interface IUserService
    {
        Task<Users> ValidateUserAsync(LoginRequest request);
        Task<bool> SignupUserAsync(SignupRequest request);
    }
}
