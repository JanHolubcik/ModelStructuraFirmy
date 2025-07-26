using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Firma : BaseModel
    {


        [Required]
        [ForeignKey(nameof(Riaditel))]
        public string? RiaditelRc { get; set; }  // FK na Zamestnanec.RodneCislo

        public Zamestnanec? Riaditel { get; set; }

        public ICollection<Divizia>? Divizie { get; set; }
    }
}
