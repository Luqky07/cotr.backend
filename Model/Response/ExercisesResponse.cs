namespace cotr.backend.Model.Response
{
    public class ExercisesResponse
    {
        public long Count { get; set; }
        public List<ExerciseData> Exercises { get; set; }

        public ExercisesResponse(List<ExerciseData> exercises)
        {
            Count = exercises.Count;
            Exercises = exercises;
        }
    }
}
