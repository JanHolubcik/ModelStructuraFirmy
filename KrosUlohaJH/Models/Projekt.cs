using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Projekt
    {
        public int Id { get; set; }

        [Required]
        public required string Nazov { get; set; }

        [Required]
        public required string Kod { get; set; }

        public int DiviziaId { get; set; }
        [Required]
        public required Divizia Divizia { get; set; }

        [ForeignKey(nameof(VeduciProjektu))]
        public string? VeduciProjektuRC { get; set; }
        public Zamestnanec? VeduciProjektu { get; set; }

        public ICollection<Oddelenie>? Oddelenia { get; set; }
      
    }
}
