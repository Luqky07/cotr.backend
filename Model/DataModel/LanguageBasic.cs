namespace cotr.backend.Model.DataModel
{
    public class LanguageBasic
    {
        public short LanguageId { get; set; }
        public string Name { get; set; }
        public string CodeStart { get; set; }

        public LanguageBasic(short languageId, string name, string codeStart)
        {
            LanguageId = languageId;
            Name = name;
            CodeStart = codeStart;
        }
    }
}
