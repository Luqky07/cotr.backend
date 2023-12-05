namespace cotr.backend.Model.DataModel
{
    public class TryException : ApiException
    {
        public string Type {  get; set; }
        public TryException(string type, string message) : base(400, message)
        {
            Type = type;
        }
    }
}
