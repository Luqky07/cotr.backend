using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;

namespace cotr.backend.Repository.Exercise
{
    public interface IExerciseRepository
    {
        Task<List<ExerciseDataResponse>> GetExercisesAsync();
        Task<Exercises> SaveExerciseAsync(Exercises exercise);
        Task<Exercises> GetExerciseByIdAsync(long exerciseId);
        Task<UserExerciseAttempts> SaveAttemptAsync(UserExerciseAttempts attempt);
        Task UpdateExerciseAsync(Exercises exercise);
        Task<ExerciseDataResponse> GetExerciseInfoByIdAsync(long exerciseId);
        Task<TestDataResponse> GetExerciseTestInfoByIdAsync(long exerciseId);
    }
}
