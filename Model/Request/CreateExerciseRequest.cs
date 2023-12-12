namespace cotr.backend.Model.Request
{
    public class CreateExerciseRequest : EditExerciseRequest
    {
        public short LanguageId { get; set; }
        public CreateExerciseRequest(short languajeId, string statement, string testCode) : base(statement, testCode)
        {
            LanguageId = languajeId;
        }
    }
}
