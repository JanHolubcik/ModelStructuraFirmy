using System.ComponentModel.DataAnnotations;

namespace KrosUlohaJH.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        [Required]
        public required string Nazov { get; set; }

        [Required]
        public required string Kod { get; set; }
    }
}
