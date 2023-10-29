using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Languajes
    {
        [Key]
        public short LanguajeId { get; }

        [Required]
        [MaxLength(50)]
        public string Name { get; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; } = string.Empty;

        public Languajes() { }

        public Languajes(short languajeId, string name, string description)
        {
            LanguajeId = languajeId;
            Name = name;
            Description = description;
        }
    }
}
