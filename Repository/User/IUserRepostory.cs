using cotr.backend.Model.Tables;

namespace cotr.backend.Repository.User
{
    public interface IUserRepostory
    {
        Task<Users> GetUserByNicknameOrEmailAsync(string data);
        Task<Users?> GetUserByNicknameAsync(string nickname);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<UserCredential> GetUserCredentialByIdAsync(int userId);
        Task<Users> SaveNewUserAsync(Users newUser);
        Task SaveNewCredentialAsync(UserCredential newCredential);
    }
}
