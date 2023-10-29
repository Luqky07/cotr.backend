using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class ExerciseResources
    {
        [Key]
        public long ResourceId { get; }

        [Required]
        [ForeignKey("Exercises")]
        public long ExerciseId { get; }

        [Required]
        [MaxLength(256)]
        public string ResourceURL { get; set; } = string.Empty;

        [Required]
        public bool IsValidResource { get; set; }

        public Exercises Exercise { get; } = new();

        public ExerciseResources() { }

        public ExerciseResources(long resourceId, long exerciseId, string resourceURL, bool isValidResource)
        {
            ResourceId = resourceId;
            ExerciseId = exerciseId;
            ResourceURL = resourceURL;
            IsValidResource = isValidResource;
        }
    }
}
