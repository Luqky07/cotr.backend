using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class ExerciseResources
    {
        [Key]
        public long ResourceId { get; }
        public long ExerciseId { get; }
        public string ResourceURL { get; set; }
        public bool IsValidResource { get; set; }

        public ExerciseResources(long resourceId, long exerciseId, string resourceURL, bool isValidResource)
        {
            ResourceId = resourceId;
            ExerciseId = exerciseId;
            ResourceURL = resourceURL;
            IsValidResource = isValidResource;
        }
    }
}
