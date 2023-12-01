using cotr.backend.Model.DataModel;

namespace cotr.backend.Model
{
    public class ExerciseData
    {
        public UserBasic Author { get; set; }
        public ExerciseBasic Exercise { get; set; }
        public LanguajeBasic Languaje { get; set; }

        public ExerciseData(UserBasic author, ExerciseBasic exercise, LanguajeBasic languaje)
        {
            Author = author;
            Exercise = exercise;
            Languaje = languaje;
        }
    }
}
