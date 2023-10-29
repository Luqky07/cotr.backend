using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Requests
    {
        [Key]
        public long RequestId { get; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; }

        [Required]
        [ForeignKey("Groups")]
        public int GroupId { get; }

        [Required]
        public DateTime RequestDate { get; }

        [Required]
        public bool IsResponded { get; set; }

        public Users User { get; } = new();
        public Groups Group { get; } = new();

        public Requests() { }

        public Requests(long requestId, int userId, int groupId, DateTime requestDate, bool isResponded)
        {
            RequestId = requestId;
            UserId = userId;
            GroupId = groupId;
            RequestDate = requestDate;
            IsResponded = isResponded;
        }
    }
}
