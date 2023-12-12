using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Languages
    {
        [Key]
        public short LanguageId { get; set; }

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

        public Languages() { }

        public Languages(short languageId, string name, string description, string codeStart, string testStart)
        {
            LanguageId = languageId;
            Name = name;
            Description = description;
            CodeStart = codeStart;
            TestStart = testStart;
        }
    }
}
