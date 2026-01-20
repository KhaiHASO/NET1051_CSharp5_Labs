using System.ComponentModel.DataAnnotations;

namespace DemoScaffoldingAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string? Name { get; set; }

        [MaxLength(250)]
        public string? StartLocation { get; set; }

        [MaxLength(250)]
        public string? EndLocation { get; set; }
    }
}
