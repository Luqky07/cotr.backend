namespace cotr.backend.Model.Request
{
    public class CreateExerciseRequest
    {
        public short LanguajeId { get; set; }
        public string Statement { get; set; }
        public string TestCode { get; set; }

        public CreateExerciseRequest(short languajeId, string statement, string testCode)
        {
            LanguajeId = languajeId;
            Statement = statement;
            TestCode = testCode;
        }
    }
}
