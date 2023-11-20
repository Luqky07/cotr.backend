using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UserExerciseAttempts
    {
        [Key]
        public long AttemptId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Exercises")]
        public long ExerciseId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime AttemptDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Result { get; set; } = string.Empty;

        public UserExerciseAttempts() { }

        public UserExerciseAttempts(int userId, long exerciseId, DateTime attemptDate, bool isCompleted, string code, string result)
        {
            UserId = userId;
            ExerciseId = exerciseId;
            AttemptDate = attemptDate;
            IsCompleted = isCompleted;
            Code = code;
            Result = result;
        }

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
