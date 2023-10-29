using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.User;

namespace cotr.backend.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepostory _userRepostory;

        public UserService(IUserRepostory userRepostory)
        {
            _userRepostory = userRepostory;
        }

        public async Task<Users> ValidateUserAsync(LoginRequest request)
        {
            try
            {
                Users userData = await _userRepostory.GetUserByNicknameOrEmailAsync(request.User);

                UserCredential credential = await _userRepostory.GetUserCredentialByIdAsync(userData.UserId);

                if (!credential.IsActive) throw new ApiException(401, "Usuario bloqueado");

                if (!BCrypt.Net.BCrypt.Verify(request.Password, credential.HashedPassword)) throw new ApiException(401, "Credenciales incorrectas");
                return userData;
            }
            catch(Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task SignupUserAsync(SignupRequest request)
        {
            try
            {
                if ((await _userRepostory.GetUserByEmailAsync(request.Email)) != null) throw new ApiException(409, "El email ya existe");
                if ((await _userRepostory.GetUserByNicknameAsync(request.Nickname)) != null) throw new ApiException(409, "El nombre de usuario ya existe");

                Users savedUser = await _userRepostory.SaveNewUserAsync(new(request.Nickname, request.Email, request.Name, request.Surname, request.SecondSurname, request.Birthdate, request.Affiliation));

                string salt = BCrypt.Net.BCrypt.GenerateSalt(4);

                await _userRepostory.SaveNewCredentialAsync(new(savedUser.UserId, salt, BCrypt.Net.BCrypt.HashPassword(request.Password, salt), DateTime.Now, 0, null, null, true));
            }
            catch(Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
