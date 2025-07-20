using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrosUlohaJH.Models
{
    public class Zamestnanec
    {
        [Key]
        [Required]
        [RegularExpression(@"^\d{6}/?\d{4}$", ErrorMessage = "Neplatný formát rodného čísla.")] //skotroluj, či má čísla a lomítko.
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Rodné číslo musí mať presne 11 znakov a lomítko.")]
        public required string RodneCislo { get; set; }

        [Required(ErrorMessage = "Meno je povinné.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Meno musí mať aspoň 2 znaky.")]
        public required string  Meno { get; set; }

        [Required(ErrorMessage = "Priezvisko je povinné")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Priezvisko musí mať aspoň 2 znaky.")]
        public required string Priezvisko { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? TelefonneCislo { get; set; }

        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatný formát emailovej adresy.")]
        public required string Email { get; set; }
        public string Titul { get; set; } = string.Empty;

        [ForeignKey(nameof(Oddelenie))]
        public int? OddelenieId { get; set; }
        public Oddelenie? Oddelenie { get; set; } //? znamená, že môže byť null
        public ICollection<Divizia>? VedeneDivizie { get; set; }
        public ICollection<Projekt>? Projekty { get; set; }
        public ICollection<Oddelenie>? Oddelenia { get; set; }

        
    }
}
