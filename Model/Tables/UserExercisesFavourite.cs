using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class UserExercisesFavourite
    {
        [Key]
        public int FavoriteId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Exercises")]
        public long ExerciseId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime AddDate { get; set; } = DateTime.UtcNow;
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
