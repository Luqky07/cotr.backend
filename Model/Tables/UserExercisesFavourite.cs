using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class UserExercisesFavourite
    {
        [Key]
        public int FavoriteId { get; }
        public int UserId { get; }
        public long ExerciseId { get; }
        public DateTime AddDate { get; }

        public UserExercisesFavourite(int favoriteId, int userId, long exerciseId, DateTime addDate)
        {
            FavoriteId = favoriteId;
            UserId = userId;
            ExerciseId = exerciseId;
            AddDate = addDate;
        }
    }
}
