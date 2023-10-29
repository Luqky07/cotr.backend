using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Members
    {
        [Key]
        public int MemberId { get; }
        public int UserId { get; }
        public int GroupId { get; }
        public short RoleId { get; set; }
        public DateTime JoinDate { get; }
    }
}
