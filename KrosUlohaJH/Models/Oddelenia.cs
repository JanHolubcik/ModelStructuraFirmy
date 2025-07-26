using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Oddelenie : BaseModel
    {
        public int? ProjektId { get; set; }
        public Projekt? Projekt { get; set; }


        [ForeignKey(nameof(VeduciOddelenia))]
        public string? VeduciOddeleniaRc { get; set; }
        public Zamestnanec? VeduciOddelenia { get; set; }

        public ICollection<Zamestnanec>? Zamestnanci { get; set; }
    }
}
