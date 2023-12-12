using cotr.backend.Model.DataModel;

namespace cotr.backend.Model.Response
{
    public class TestDataResponse
    {
        public UserBasic Author { get; set; }
        public TestBasic Test { get; set; }
        public LanguageBasic Language { get; set; }

        public TestDataResponse(UserBasic author, TestBasic test, LanguageBasic language)
        {
            Author = author;
            Test = test;
            Language = language;
        }
    }
}
