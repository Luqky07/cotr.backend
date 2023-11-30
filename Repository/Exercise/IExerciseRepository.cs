using cotr.backend.Model;
using cotr.backend.Model.Tables;

namespace cotr.backend.Repository.Exercise
{
    public interface IExerciseRepository
    {
        Task<List<ExerciseData>> GetExercisesAsync();
        Task<List<ExerciseData>> GetExercisesCreatedAsync(int userId);
        Task<Exercises> SaveExerciseAsync(Exercises exercise);
        Task<Exercises> GetExerciseByIdAsync(long exerciseId);
        Task<UserExerciseAttempts> SaveAttemptAsync(UserExerciseAttempts attempt);
        Task UpdateExerciseAsync(Exercises exercise);
    }
}
