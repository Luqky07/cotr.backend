using cotr.backend.Model.DataModel;

namespace cotr.backend.Model.Response
{
    public class ExerciseDataResponse
    {
        public UserBasic Author { get; set; }
        public ExerciseBasic Exercise { get; set; }
        public LanguageBasic Language { get; set; }

        public ExerciseDataResponse(UserBasic author, ExerciseBasic exercise, LanguageBasic language)
        {
            Author = author;
            Exercise = exercise;
            Language = language;
        }
    }
}
