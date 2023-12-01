namespace cotr.backend.Model.DataModel
{
    public class LanguajeBasic
    {
        public short LanguajeId { get; set; }
        public string Name { get; set; }
        public string CodeStart { get; set; }

        public LanguajeBasic(short languajeId, string name, string codeStart)
        {
            LanguajeId = languajeId;
            Name = name;
            CodeStart = codeStart;
        }
    }
}
