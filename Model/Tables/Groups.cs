using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Groups
    {
        [Key]
        public int GroupId { get; }
        public string Name { get; set; }
        public DateTime CreationDate { get; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }

        public Groups(int groupId, string name, DateTime creationDate, string description, bool isPublic)
        {
            GroupId = groupId;
            Name = name;
            CreationDate = creationDate;
            Description = description;
            IsPublic = isPublic;
        }
    }
}
