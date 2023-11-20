using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;

namespace cotr.backend.Service.Exercise
{
    public interface IExerciseService
    {
        Task<ExercisesResponse> GetExercises(string? statement, string? author);
        Task<Exercises> CreateNewExercise(int userId, CreateExerciseRequest request);
        Task TryExerciseAttemptAsync(int userId, long exerciseId, AttemptRequest request);
    }
}
