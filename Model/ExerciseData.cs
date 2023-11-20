namespace cotr.backend.Model
{
    public class ExerciseData
    {
        public string Author { get; set; }
        public string Languaje { get; set; }
        public string Statement { get; set; }
        public DateTime CreationDate { get; set; }

        public ExerciseData(string author, string languaje, string statement, DateTime creationDate)
        {
            Author = author;
            Languaje = languaje;
            Statement = statement;
            CreationDate = creationDate;
        }
    }
}
