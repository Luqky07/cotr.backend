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

                if (_encryptService.ValidatePassword(request.Password, credential.HashedPassword))
                {
                    credential.LastLogin = DateTime.Now;
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

                if (!PasswordRegex().IsMatch(request.Password)) throw new ApiException(409, "La contraseña no cumple con los requisitos de seguridad. Su longitud debe ser de mínimo 8 caracteres y debe al menos contener una letrá en mayúscula y números");

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

        public async Task UpdatePasswordAsync( UpdatePasswordRequest updatePassword)
        {
            UserCredential credentials = await _userRepostory.GetUserCredentialByResetToken(updatePassword.Token) ?? throw new ApiException(404, "No se ha encontrado un usuario asociado a ese token");
            if (DateTime.Now > credentials.ResetTokenExpiration) throw new ApiException(401, "El token para realizar el cambio de cuenta ha expirado, solicite uno nuevo");
            if (!PasswordRegex().IsMatch(updatePassword.Password)) throw new ApiException(409, "La contraseña no cumple con los requisitos de seguridad. Su longitud debe ser de mínimo 8 caracteres y debe al menos contener una letrá en mayúscula y números");

            string salt = _encryptService.GenerateSalt();
            string hashedpassword = _encryptService.EncryptPassword(updatePassword.Password, salt);

            credentials.ResetToken = null;
            credentials.ResetTokenExpiration = null;
            credentials.IsActive = true;
            credentials.Salt = salt;
            credentials.HashedPassword = hashedpassword;
            credentials.FailedLoginAttempts = 0;

            await _userRepostory.UpdateCredentialsAsync(credentials);
        }

        public async Task<EmailMessage> RecoverPasswordAsync(string email)
        {
            Users user = await _userRepostory.GetUserByEmailAsync(email) ?? throw new ApiException(404, "No se ha encontrado un usuario asociado a ese email");

            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new();

            string token = "";
            while (await _userRepostory.GetUserCredentialByResetToken(token) != null)
            {
                token = new(Enumerable.Repeat(CHARS, 15)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
            }
            
            UserCredential credential = await _userRepostory.GetUserCredentialByIdAsync(user.UserId);

            credential.ResetToken = token;
            credential.ResetTokenExpiration = DateTime.Now.AddDays(1);

            await _userRepostory.UpdateCredentialsAsync(credential);

            string body = $"<p>Hola <strong>{user.Name}</strong>,<br>Puedes cambiar tu contraseña accediendo al siguiente enlace:</p><p><a href='https://blue-rock-0344a9c03.4.azurestaticapps.net/change-password?token={token}'>Cambia tu contraseña</a></p><p>Si no has solicitado el cambio ponte en contacto con nosotros</p>";

            return new(user.Email, "Cambia tu contraseña", body);
        }
    }
}
