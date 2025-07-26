using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Projekt : BaseModel
    {
        public int? DiviziaId { get; set; }

        public Divizia? Divizia { get; set; }

        [ForeignKey(nameof(VeduciProjektu))]
        public string? VeduciProjektuRC { get; set; }
        public Zamestnanec? VeduciProjektu { get; set; }

        public ICollection<Oddelenie>? Oddelenia { get; set; }

    }
}
