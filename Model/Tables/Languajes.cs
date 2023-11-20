using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Languajes
    {
        [Key]
        public short LanguajeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string CodeStart { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string TestStart { get; set; } = string.Empty;

        public Languajes() { }

        public Languajes(short languajeId, string name, string description, string codeStart, string testStart)
        {
            LanguajeId = languajeId;
            Name = name;
            Description = description;
            CodeStart = codeStart;
            TestStart = testStart;
        }
    }
}
