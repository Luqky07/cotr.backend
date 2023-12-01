namespace cotr.backend.Model.Response
{
    public class ExercisesResponse
    {
        public long Count { get; set; }
        public List<ExerciseDataResponse> Exercises { get; set; }

        public ExercisesResponse(List<ExerciseDataResponse> exercises)
        {
            Count = exercises.Count;
            Exercises = exercises;
        }
    }
}
