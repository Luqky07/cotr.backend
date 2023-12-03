using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;

namespace cotr.backend.Service.Exercise
{
    public interface IExerciseService
    {
        Task<ExercisesResponse> GetExercisesAsync(int userId, string? statement, string? author, short? languajeId, int? creatorId);
        Task<ExercisesResponse> GetExercisesCreatedAsync(int userId);
        Task<Exercises> CreateNewExercise(int userId, CreateExerciseRequest request);
        Task TryExerciseAttemptAsync(int userId, long exerciseId, AttemptRequest request);
        Task<ExerciseDataResponse> GetExerciseInfoByIdAsync(int userId, long exerciseId);
    }
}
