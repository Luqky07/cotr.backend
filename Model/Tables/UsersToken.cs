using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UsersToken
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(15)]
        public string Token { get; set; } = string.Empty;

        [Required]
        public DateTime TokenExpirationDate { get; set; }

        [Required]
        public DateTime TokenCreationDate { get; set; }

        [Required]
        [MaxLength(16)]
        public string IpDirection { get; set; } = string.Empty;

        public Users User { get; set; } = new();

        public UsersToken() { }

        public UsersToken(int userId, string token, DateTime tokenExpirationDate, DateTime tokenCreationDate, string ipDirection)
        {
            UserId = userId;
            Token = token;
            TokenExpirationDate = tokenExpirationDate;
            TokenCreationDate = tokenCreationDate;
            IpDirection = ipDirection;
        }
    }
}
