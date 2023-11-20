using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Requests
    {
        [Key]
        public long RequestId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Groups")]
        public int GroupId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsResponded { get; set; }

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
