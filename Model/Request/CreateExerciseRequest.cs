namespace cotr.backend.Model.Request
{
    public class CreateExerciseRequest : EditExerciseRequest
    {
        public short LanguajeId { get; set; }
        public CreateExerciseRequest(short languajeId, string statement, string testCode) : base(statement, testCode)
        {
            LanguajeId = languajeId;
        }
    }
}
