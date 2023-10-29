using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Exercises
    {
        [Key]
        public long ExerciseId { get; }

        [Required]
        [ForeignKey("Users")]
        public int CreatorId { get; }

        [Required]
        [ForeignKey("Languajes")]
        public short LanguajeId { get; }

        [Required]
        [MaxLength(1000)]
        public string Statement { get; set; } = string.Empty;

        [Required]
        public bool IsAproved { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public DateTime CreationDate { get; }

        public Users Creator { get; } = new();
        public Languajes Languaje { get; } = new();

        public Exercises() { }

        public Exercises(long exerciseId, int creatorId, short languajeId, string statement, bool isAproved, bool isPublic, DateTime creationDate)
        {
            ExerciseId = exerciseId;
            CreatorId = creatorId;
            LanguajeId = languajeId;
            Statement = statement;
            IsAproved = isAproved;
            IsPublic = isPublic;
            CreationDate = creationDate;
        }
    }
}
