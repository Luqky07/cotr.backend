using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class GroupUsersBlocked
    {
        [Key]
        public int BlockedId { get; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; }

        [Required]
        [ForeignKey("Groups")]
        public int GroupId { get; }

        [Required]
        public DateTime BlockDate { get; }

        public Users User { get; } = new();
        public Groups Group { get; } = new();

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
