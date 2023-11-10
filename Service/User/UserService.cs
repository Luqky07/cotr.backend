using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.User;
using cotr.backend.Service.Encrypt;
using System.Text.RegularExpressions;

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
                if (await _userRepostory.GetUserByEmailAsync(request.Email) != null) throw new ApiException(409, "El email ya existe");
                if (await _userRepostory.GetUserByNicknameAsync(request.Nickname) != null) throw new ApiException(409, "El nombre de usuario ya existe");
                if (request.Birthdate > DateTime.Today.AddYears(-16)) throw new ApiException(409, "No puedes registrarte en la plataforma si eres menor de 16 años");

                if (!Regex.IsMatch(request.Password, "^(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]{8,}$")) throw new ApiException(409, "La contraseña no cumple con los requisitos de seguridad. Su longitud debe ser de mínimo 8 caracteres y debe al menos contener una letrá en mayúscula y números");

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
