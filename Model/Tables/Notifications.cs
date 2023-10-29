using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Notifications
    {
        [Key]
        public long NotificationId { get; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public bool IsSeen { get; set; }

        public Notifications(long notificationId, int userId, string description, bool isSeen)
        {
            NotificationId = notificationId;
            UserId = userId;
            Description = description;
            IsSeen = isSeen;
        }
    }
}
