namespace cotr.backend.Model.Response
{
    public class ExerciseInfoResponse
    {
        public long ExerciseId { get; set; }
        public string ExerciseLanguje { get; set; }
        public string CodeStart { get; set; }
        public string Statement { get; set; }
        public string Creator { get; set; }

        public ExerciseInfoResponse(long exerciseId, string exerciseLanguje, string codeStart, string statement, string creator)
        {
            ExerciseId = exerciseId;
            ExerciseLanguje = exerciseLanguje;
            CodeStart = codeStart;
            Statement = statement;
            Creator = creator;
        }
    }
}