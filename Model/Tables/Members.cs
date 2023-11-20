using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Members
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Groups")]
        public int GroupId { get; set; }

        [Required]
        [ForeignKey("Roles")]
        public short RoleId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public Members() { }

        public Members(int memberId, int userId, int groupId, short roleId, DateTime joinDate)
        {
            MemberId = memberId;
            UserId = userId;
            GroupId = groupId;
            RoleId = roleId;
            JoinDate = joinDate;
        }
    }
}
