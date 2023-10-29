using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class ExercisesTests
    {
        [Key]
        public long TestId { get; }

        [Required]
        [ForeignKey("Exercises")]
        public long ExerciseId { get; }

        [Required]
        [MaxLength(1000)]
        public string Input { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Output { get; set; } = string.Empty;

        [Required]
        public DateTime AddDate { get; set; }

        public Exercises Exercise { get; } = new();

        public ExercisesTests() { }

        public ExercisesTests(long testId, long exerciseId, string input, string output, DateTime addDate)
        {
            TestId = testId;
            ExerciseId = exerciseId;
            Input = input;
            Output = output;
            AddDate = addDate;
        }
    }
}
