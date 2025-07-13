using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Firma
    {
        //[Key] ak je to pomenované ako id tak key nemusím používať
        public int Id { get; set; }
        [Required]
        public required string Nazov { get; set; }

        [Required]
        public required string Kod { get; set; }

        [Required]
        [ForeignKey(nameof(Riaditel))]
        public string? RiaditelRc { get; set; }  // FK na Zamestnanec.RodneCislo

        public Zamestnanec? Riaditel { get; set; } 

        public ICollection<Divizia>? Divizie { get; set; }
    }
}
