using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Requests
    {
        [Key]
        public long RequestId { get; }
        public int UserId { get; }
        public int GroupId { get; }
        public DateTime RequestDate { get; }
        public bool IsResponded { get; set; }

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
