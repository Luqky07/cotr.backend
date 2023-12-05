using cotr.backend.Model.DataModel;

namespace cotr.backend.Model.Request
{
    public class EditExerciseRequest
    {
        public string Statement { get; set; }
        public string TestCode { get; set; }

        public EditExerciseRequest(string statement, string testCode)
        {
            Statement = statement;
            TestCode = testCode;
        }
    }
}
