using System.ComponentModel.DataAnnotations;

namespace Lab6Bai2.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string StartLocation { get; set; } = string.Empty;

        [StringLength(250)]
        public string EndLocation { get; set; } = string.Empty;
    }
}
