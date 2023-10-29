using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Groups
    {
        [Key]
        public int GroupId { get; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime CreationDate { get; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsPublic { get; set; }

        public Groups() { }

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
