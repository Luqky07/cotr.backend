using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class UserExerciseAttempts
    {
        [Key]
        public long AttemptId { get; }
        public int UserId { get; }
        public long ExerciseId { get; }
        public DateTime AttemptDate { get; }
        public bool IsCompleted { get; }
        public string Code { get; }
        public string Result { get; }

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
