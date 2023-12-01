using cotr.backend.Model.DataModel;

namespace cotr.backend.Model.Response
{
    public class ExerciseDataResponse
    {
        public UserBasic Author { get; set; }
        public ExerciseBasic Exercise { get; set; }
        public LanguajeBasic Languaje { get; set; }

        public ExerciseDataResponse(UserBasic author, ExerciseBasic exercise, LanguajeBasic languaje)
        {
            Author = author;
            Exercise = exercise;
            Languaje = languaje;
        }
    }
}
