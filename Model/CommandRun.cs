namespace cotr.backend.Model
{
    public class CommandRun
    {
        public string Result { get; set; }
        public string Error { get; set; }

        public CommandRun(string result, string error)
        {
            Result = result;
            Error = error;
        }
    }
}
