using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Exercises
    {
        [Key]
        public long ExerciseId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int CreatorId { get; set; }

        [Required]
        [ForeignKey("Languajes")]
        public short LanguajeId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Statement { get; set; } = string.Empty;

        [Required]
        public bool IsAproved { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string TestCode { get; set; } = string.Empty;

        [Required]
        public string TestClassName { get; set; } = string.Empty;

        public Exercises() { }

        public Exercises(int creatorId, short languajeId, string statement, bool isAproved, bool isPublic, DateTime creationDate, string testCode, string testClassName)
        {
            CreatorId = creatorId;
            LanguajeId = languajeId;
            Statement = statement;
            IsAproved = isAproved;
            IsPublic = isPublic;
            CreationDate = creationDate;
            TestCode = testCode;
            TestClassName = testClassName;
        }

        public Exercises(long exerciseId, int creatorId, short languajeId, string statement, bool isAproved, bool isPublic, DateTime creationDate, string testCode, string testClassName)
        {
            ExerciseId = exerciseId;
            CreatorId = creatorId;
            LanguajeId = languajeId;
            Statement = statement;
            IsAproved = isAproved;
            IsPublic = isPublic;
            CreationDate = creationDate;
            TestCode = testCode;
            TestClassName = testClassName;
        }
    }
}
