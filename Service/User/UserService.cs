using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace cotr.backend.Service.User
{
    public class UserService : IUserService
    {
        private readonly CotrContext _context;

        public UserService(CotrContext context)
        {
            _context = context;
        }

        public async Task<Users> ValidateUserAsync(LoginRequest request)
        {
            try
            {
                Users userData = await _context.Users.FirstOrDefaultAsync(x => x.Nickname.Equals(request.User) || x.Email.Equals(request.User)) ?? throw new ApiException(404, "Usuario no encontrado");

                UserCredential credential = await _context.UserCredential.FirstAsync(x => x.UserId.Equals(userData.UserId));

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
        public async Task<bool> SignupUserAsync(SignupRequest request)
        {
            try
            {
                Users? userData = await _context.Users.FirstOrDefaultAsync(x => x.Nickname.Equals(request.Nickname) || x.Email.Equals(request.Email));

                if (userData != null)
                {
                    if (userData.Email.Equals(request.Email)) throw new ApiException(409, "El email ya existe");
                    if (userData.Nickname.Equals(request.Nickname)) throw new ApiException(409, "El nombre de usuario ya existe");
                }

                Users newUser = new(request.Nickname, request.Email, request.Name, request.Surname, request.SecondSurname, request.Birthdate, request.Affiliation);

                var save = await _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                string salt = BCrypt.Net.BCrypt.GenerateSalt(4);

                int hashPassword = (BCrypt.Net.BCrypt.HashPassword(request.Password, salt)).Length;

                UserCredential userCredential = new(save.Entity.UserId, salt, BCrypt.Net.BCrypt.HashPassword(request.Password, salt), DateTime.Now, 0, null, null, true);

                await _context.UserCredential.AddAsync(userCredential);

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
