using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class GroupUsersBlocked
    {
        [Key]
        public int BlockedId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Groups")]
        public int GroupId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime BlockDate { get; set; } = DateTime.UtcNow;

        public GroupUsersBlocked() { }

        public GroupUsersBlocked(int blockedId, int userId, int groupId, DateTime blockDate)
        {
            BlockedId = blockedId;
            UserId = userId;
            GroupId = groupId;
            BlockDate = blockDate;
        }
    }
}
