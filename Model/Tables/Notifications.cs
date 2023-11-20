using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Notifications
    {
        [Key]
        public long NotificationId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public bool IsSeen { get; set; }

        public Notifications() { }
        public Notifications(long notificationId, int userId, string description, bool isSeen)
        {
            NotificationId = notificationId;
            UserId = userId;
            Description = description;
            IsSeen = isSeen;
        }
    }
}
