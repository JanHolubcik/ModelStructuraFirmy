using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Divizia : BaseModel
    {

        public int? FirmaId { get; set; }
        public Firma? Firma { get; set; }

        [ForeignKey(nameof(Veduci))]
        public string? VeduciRC { get; set; }
        public Zamestnanec? Veduci { get; set; } 

        public ICollection<Projekt>? Projekty { get; set; }
    }
}
