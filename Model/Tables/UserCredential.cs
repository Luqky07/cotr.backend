using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UserCredential
    {
        [Key]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(60)]
        public string HashedPassword { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

        [Required]
        public short FailedLoginAttempts { get; set; }

        [MaxLength(15)]
        public string? ResetToken { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ResetTokenExpiration { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public UserCredential() { }

        public UserCredential(int userId, string hashedPassword, DateTime lastLogin, short failedLoginAttempts, string? resetToken, DateTime? resetTokenExpiration, bool isActive)
        {
            UserId = userId;
            HashedPassword = hashedPassword;
            LastLogin = lastLogin;
            FailedLoginAttempts = failedLoginAttempts;
            ResetToken = resetToken;
            ResetTokenExpiration = resetTokenExpiration;
            IsActive = isActive;
        }
    }
}
