using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class ExercisesTests
    {
        [Key]
        public long TestId { get; }
        public long ExerciseId { get; }
        public string Input { get; set; }
        public string Output { get; set; }
        public DateTime AddDate { get; set; }

        public ExercisesTests(long testId, long exerciseId, string input, string output, DateTime addDate)
        {
            TestId = testId;
            ExerciseId = exerciseId;
            Input = input;
            Output = output;
            AddDate = addDate;
        }
    }
}
