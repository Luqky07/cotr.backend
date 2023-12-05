namespace cotr.backend.Model.DataModel
{
    public class TestBasic
    {
        public long ExerciseId { get; set; }
        public string Statement { get; set; }
        public string TestCode { get; set; }
        public bool IsAproved { get; set; }

        public TestBasic(long exerciseId, string statement, string testCode, bool isAproved)
        {
            ExerciseId = exerciseId;
            TestCode = testCode;
            Statement = statement;
            IsAproved = isAproved;
        }
    }
}
