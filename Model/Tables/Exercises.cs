using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Exercises
    {
        [Key]
        public long ExerciseId { get; }
        public int CreatorId { get; }
        public short LanguajeId { get; }
        public string Statement { get; set; }
        public bool IsAproved { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreationDate { get; }

        public Exercises(long exerciseId, int creatorId, short languajeId, string statement, bool isAproved, bool isPublic, DateTime creationDate)
        {
            ExerciseId = exerciseId;
            CreatorId = creatorId;
            LanguajeId = languajeId;
            Statement = statement;
            IsAproved = isAproved;
            IsPublic = isPublic;
            CreationDate = creationDate;
        }
    }
}
