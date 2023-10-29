using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.User;
using cotr.backend.Service.Encrypt;

namespace cotr.backend.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepostory _userRepostory;
        private readonly IEncryptService _encryptService;

        public UserService(IUserRepostory userRepostory, IEncryptService encryptService)
        {
            _userRepostory = userRepostory;
            _encryptService = encryptService;
        }

        public async Task<Users> ValidateUserAsync(LoginRequest request)
        {
            try
            {
                Users userData = await _userRepostory.GetUserByNicknameOrEmailAsync(request.User);

                UserCredential credential = await _userRepostory.GetUserCredentialByIdAsync(userData.UserId);

                if (!credential.IsActive) throw new ApiException(401, "Usuario bloqueado");

                if (!_encryptService.ValidatePassword(request.Password, credential.HashedPassword)) throw new ApiException(401, "Credenciales incorrectas");
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

                string salt = _encryptService.GenerateSalt();
                string hashedpassword = _encryptService.EncryptPassword(request.Password, salt);

                await _userRepostory.SaveNewCredentialAsync(new(savedUser.UserId, salt, hashedpassword, DateTime.Now, 0, null, null, true));
            }
            catch(Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
