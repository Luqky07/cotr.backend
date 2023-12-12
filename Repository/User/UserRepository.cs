using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace cotr.backend.Repository.User
{
    public class UserRepository : IUserRepostory
    {
        private readonly CotrContext _context;

        public UserRepository(CotrContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByNicknameOrEmailAsync(string data)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Nickname.Equals(data) || x.Email.Equals(data)) ?? throw new ApiException(404, "Usuario no encontrado");
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task<Users?> GetUserByNicknameAsync(string nickname)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Nickname.Equals(nickname));
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task<UserCredential> GetUserCredentialByIdAsync(int userId)
        {
            try
            {
                return await _context.UserCredential.FirstOrDefaultAsync(x => x.UserId.Equals(userId)) ?? throw new ApiException(404, "Usuario no encontrado");
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task<Users> SaveNewUserAsync(Users newUser)
        {
            try
            {
                var save = await _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                return save.Entity;
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task SaveNewCredentialAsync(UserCredential newCredential)
        {
            try
            {
                var save = await _context.UserCredential.AddAsync(newCredential);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task UpdateCredentialsAsync (UserCredential credential)
        {
            try
            {
                _context.UserCredential.Update(credential);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<Users> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.EmailIsVerified) ?? throw new ApiException(404, "Usuario no encontrado");
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task UpdateUsersAsync(Users user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<UserCredential?> GetUserCredentialByResetToken(string resetToken)
        {
            try
            {
                return await _context.UserCredential.FirstOrDefaultAsync(x => x.ResetToken != null && x.ResetToken.Equals(resetToken));
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            };
        }

        public async Task DeleteUserAsync(Users user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<Users?> GetUserByEmailToken(string emailToken)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.EmailToken != null && x.EmailToken.Equals(emailToken));
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            };
        }
    }
}
