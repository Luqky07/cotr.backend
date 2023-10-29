using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class GroupUsersBlocked
    {
        [Key]
        public int BlockedId { get; }
        public int UserId { get; }
        public int GroupId { get; }
        public DateTime BlockDate { get; }

        public GroupUsersBlocked(int blockedId, int userId, int groupId, DateTime blockDate)
        {
            BlockedId = blockedId;
            UserId = userId;
            GroupId = groupId;
            BlockDate = blockDate;
        }
    }
}
