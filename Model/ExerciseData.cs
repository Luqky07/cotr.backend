namespace cotr.backend.Model
{
    public class ExerciseData
    {
        public long ExerciseId { get; set; }
        public string Author { get; set; }
        public string Languaje { get; set; }
        public string Statement { get; set; }
        public DateTime CreationDate { get; set; }

        public ExerciseData(long exerciseId, string author, string languaje, string statement, DateTime creationDate)
        {
            ExerciseId = exerciseId;
            Author = author;
            Languaje = languaje;
            Statement = statement;
            CreationDate = creationDate;
        }
    }
}
