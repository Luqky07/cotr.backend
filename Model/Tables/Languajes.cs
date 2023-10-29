using System.ComponentModel.DataAnnotations;

namespace cotr.backend.Model.Tables
{
    public class Languajes
    {
        [Key]
        public short LanguajeId { get; }
        public string Name { get; }
        public string Description { get; }

        public Languajes(short languajeId, string name, string description)
        {
            LanguajeId = languajeId;
            Name = name;
            Description = description;
        }
    }
}
