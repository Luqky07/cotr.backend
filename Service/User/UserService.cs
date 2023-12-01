using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.User;
using cotr.backend.Service.Encrypt;
using System.Text.RegularExpressions;

namespace cotr.backend.Service.User
{
    public partial class UserService : IUserService
    {
        [GeneratedRegex("^(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]{8,}$")]
        private static partial Regex PasswordRegex();

        private readonly IUserRepostory _userRepostory;
        private readonly ISecutiryService _secutiryService;

        public UserService(IUserRepostory userRepostory, ISecutiryService secutiryService)
        {
            _userRepostory = userRepostory;
            _secutiryService = secutiryService;
        }

        public async Task<Users> ValidateUserAsync(LoginRequest request)
        {
            Users userData = await _userRepostory.GetUserByNicknameOrEmailAsync(request.User);

            UserCredential credential = await _userRepostory.GetUserCredentialByIdAsync(userData.UserId);

            if (!credential.IsActive) throw new ApiException(401, "Usuario bloqueado, para volver a acceder a la aplicación debe cambiar su contraseña");

            if (_secutiryService.ValidatePassword(request.Password, credential.HashedPassword))
            {
                credential.LastLogin = DateTime.UtcNow;
                if (credential.FailedLoginAttempts != 0) credential.FailedLoginAttempts = 0;
                await _userRepostory.UpdateCredentialsAsync(credential);
                return userData;
            }

            if(++credential.FailedLoginAttempts >= 5)
            {
                credential.IsActive = false;
                await _userRepostory.UpdateCredentialsAsync(credential);
                throw new ApiException(401, "El usuario ha sido bloqueado por exceso de intentos con contraseña incorrecta");
            }

            await _userRepostory.UpdateCredentialsAsync(credential);
            throw new ApiException(401, "Credenciales incorrectas");
        }

        public async Task SignupUserAsync(SignupRequest request)
        {
            if (await _userRepostory.GetUserByEmailAsync(request.Email) != null) throw new ApiException(409, "El email ya existe");
            if (await _userRepostory.GetUserByNicknameAsync(request.Nickname) != null) throw new ApiException(409, "El nombre de usuario ya existe");
            if (request.Birthdate > DateTime.Today.AddYears(-16)) throw new ApiException(409, "No puedes registrarte en la plataforma si eres menor de 16 años");

            if (!PasswordRegex().IsMatch(request.Password)) throw new ApiException(409, "La contraseña no cumple con los requisitos de seguridad. Su longitud debe ser de mínimo 8 caracteres y debe al menos contener una letrá en mayúscula y números");

            Users savedUser = await _userRepostory.SaveNewUserAsync(new(request.Nickname, request.Email, request.Name, request.Surname, request.SecondSurname, request.Birthdate, request.Affiliation));

            string hashedpassword = _secutiryService.EncryptPassword(request.Password);
            try
            {
                await _userRepostory.SaveNewCredentialAsync(new(savedUser.UserId, hashedpassword, DateTime.UtcNow, 0, null, null, true));
            }
            catch (ApiException ex)
            {
                await _userRepostory.DeleteUserAsync(savedUser);
                throw ex;
            }
            
        }

        public async Task UpdatePasswordAsync(UpdatePasswordRequest updatePassword)
        {
            if (updatePassword.Token.Length < 15 || updatePassword.Token.Contains(" ")) throw new ApiException(409, "El token no es válido");
            UserCredential credentials = await _userRepostory.GetUserCredentialByResetToken(updatePassword.Token) ?? throw new ApiException(404, "No se ha encontrado un usuario asociado a ese token");
            if (DateTime.UtcNow > credentials.ResetTokenExpiration) throw new ApiException(401, "El token para realizar el cambio de cuenta ha expirado, solicite uno nuevo");
            if (!PasswordRegex().IsMatch(updatePassword.Password)) throw new ApiException(409, "La contraseña no cumple con los requisitos de seguridad. Su longitud debe ser de mínimo 8 caracteres y debe al menos contener una letrá en mayúscula y números");

            string hashedpassword = _secutiryService.EncryptPassword(updatePassword.Password);

            credentials.ResetToken = null;
            credentials.ResetTokenExpiration = null;
            credentials.IsActive = true;
            credentials.HashedPassword = hashedpassword;
            credentials.FailedLoginAttempts = 0;

            await _userRepostory.UpdateCredentialsAsync(credentials);
        }

        public async Task<EmailMessage> RecoverPasswordAsync(string email)
        {
            Users user = await _userRepostory.GetUserByEmailAsync(email) ?? throw new ApiException(404, "No se ha encontrado un usuario asociado a ese email");

            string token;
            do
            {
                token = _secutiryService.RandomTokenRecoverPassword();
            } while (await _userRepostory.GetUserCredentialByResetToken(token) != null);
            UserCredential credential = await _userRepostory.GetUserCredentialByIdAsync(user.UserId);

            credential.ResetToken = token;
            credential.ResetTokenExpiration = DateTime.UtcNow.AddDays(1);

            await _userRepostory.UpdateCredentialsAsync(credential);

            string body = $"<p>Hola <strong>{user.Name}</strong>,<br>Puedes cambiar tu contraseña accediendo al siguiente enlace:</p><p><a href='https://blue-rock-0344a9c03.4.azurestaticapps.net/change-password?token={token}'>Cambia tu contraseña</a></p><p>Si no has solicitado el cambio ponte en contacto con nosotros</p>";

            return new(user.Email, "Cambia tu contraseña", body);
        }

        public async Task<Users> GetUserInfoByIdAsync(int userId)
        {
            return await _userRepostory.GetUserByIdAsync(userId);
        }
    }
}
