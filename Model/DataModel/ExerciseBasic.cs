namespace cotr.backend.Model.DataModel
{
    public class ExerciseBasic
    {
        public long ExerciseId { get; set; }
        public string Statement { get; set; }
        public bool IsAproved { get; set; }
        public DateTime CreationDate { get; set; }

        public ExerciseBasic(long exerciseId, string statement, bool isAproved, DateTime creationDate)
        {
            ExerciseId = exerciseId;
            Statement = statement;
            IsAproved = isAproved;
            CreationDate = creationDate;
        }
    }
}