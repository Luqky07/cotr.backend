using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UserExerciseAttempts
    {
        [Key]
        public long AttemptId { get; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; }

        [Required]
        [ForeignKey("Exercises")]
        public long ExerciseId { get; }

        [Required]
        public DateTime AttemptDate { get; }

        [Required]
        public bool IsCompleted { get; }

        [Required]
        public string Code { get; } = string.Empty;

        [Required]
        public string Result { get; } = string.Empty;

        public Users User { get; } = new();

        public Exercises Exercise { get; } = new();

        public UserExerciseAttempts() { }

        public UserExerciseAttempts(long attemptId, int userId, long exerciseId, DateTime attemptDate, bool isCompleted, string code, string result)
        {
            AttemptId = attemptId;
            UserId = userId;
            ExerciseId = exerciseId;
            AttemptDate = attemptDate;
            IsCompleted = isCompleted;
            Code = code;
            Result = result;
        }
    }
}
