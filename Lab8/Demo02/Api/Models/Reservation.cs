using System.ComponentModel.DataAnnotations;

namespace Demo02.Api.Models
{
    // Model Reservation đơn giản
    public class Reservation
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string StartLocation { get; set; }
        
        [Required]
        public string EndLocation { get; set; }
    }
}
