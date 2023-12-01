namespace cotr.backend.Model.DataModel
{
    public class LanguajeBasic
    {
        public short LanguajeId { get; set; }
        public string Name { get; set; }

        public LanguajeBasic(short languajeId, string name)
        {
            LanguajeId = languajeId;
            Name = name;
        }
    }
}
