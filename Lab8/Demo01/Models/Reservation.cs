using System.ComponentModel.DataAnnotations;

namespace Demo01.Models
{
    // Mô phỏng Slide 10
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
    }
}
