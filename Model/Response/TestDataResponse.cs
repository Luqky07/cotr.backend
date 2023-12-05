using cotr.backend.Model.DataModel;

namespace cotr.backend.Model.Response
{
    public class TestDataResponse
    {
        public UserBasic Author { get; set; }
        public TestBasic Test { get; set; }
        public LanguajeBasic Languaje { get; set; }

        public TestDataResponse(UserBasic author, TestBasic test, LanguajeBasic languaje)
        {
            Author = author;
            Test = test;
            Languaje = languaje;
        }
    }
}
