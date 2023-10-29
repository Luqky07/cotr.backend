using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UserCredential
    {
        [Key]
        [ForeignKey("Users")]
        public int UserId { get; }

        [Required]
        [MaxLength(32)]
        public string Salt { get; set; } = string.Empty;

        [Required]
        [MaxLength(60)]
        public string HashedPassword { get; set; } = string.Empty;

        [Required]
        public DateTime LastLogin { get; set; }

        [Required]
        public short FailedLoginAttempts { get; set; }

        [MaxLength(15)]
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Users User { get; } = new();

        public UserCredential() { }

        public UserCredential(int userId, string salt, string hashedPassword, DateTime lastLogin, short failedLoginAttempts, string? resetToken, DateTime? resetTokenExpiration, bool isActive)
        {
            UserId = userId;
            Salt = salt;
            HashedPassword = hashedPassword;
            LastLogin = lastLogin;
            FailedLoginAttempts = failedLoginAttempts;
            ResetToken = resetToken;
            ResetTokenExpiration = resetTokenExpiration;
            IsActive = isActive;
        }
    }
}
