using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.RegularExpressions;

namespace cotr.backend.Model.Tables
{
    public class Members
    {
        [Key]
        public int MemberId { get; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; }

        [Required]
        [ForeignKey("Groups")]
        public int GroupId { get; }

        [Required]
        [ForeignKey("Roles")]
        public short RoleId { get; set; }

        [Required]
        public DateTime JoinDate { get; }

        public Users User { get; } = new();
        public Groups Group { get; } = new();
        public Roles Role { get; } = new();

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
