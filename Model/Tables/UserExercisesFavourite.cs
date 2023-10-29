using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UserExercisesFavourite
    {
        [Key]
        public int FavoriteId { get; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; }

        [Required]
        [ForeignKey("Exercises")]
        public long ExerciseId { get; }

        [Required]
        public DateTime AddDate { get; }

        public Users User { get; } = new();

        public Exercises Exercise { get; } = new();

        public UserExercisesFavourite() { }

        public UserExercisesFavourite(int favoriteId, int userId, long exerciseId, DateTime addDate)
        {
            FavoriteId = favoriteId;
            UserId = userId;
            ExerciseId = exerciseId;
            AddDate = addDate;
        }
    }
}
