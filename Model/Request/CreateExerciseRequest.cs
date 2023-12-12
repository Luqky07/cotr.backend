namespace cotr.backend.Model.Request
{
    public class CreateExerciseRequest : EditExerciseRequest
    {
        public short LanguageId { get; set; }
        public CreateExerciseRequest(short languageId, string statement, string testCode) : base(statement, testCode)
        {
            LanguageId = languageId;
        }
    }
}
