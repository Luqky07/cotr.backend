using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Roles
    {
        [Key]
        public short RoleId { get; }

        [Required]
        [MaxLength(100)]
        public string Description { get; } = string.Empty;

        public Roles() { }

        public Roles(short roleId, string description)
        {
            RoleId = roleId;
            Description = description;
        }
    }
}
