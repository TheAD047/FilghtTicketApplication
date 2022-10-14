using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Airline
    {
        public int airlineID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string? airlineName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? airlineEmail { get; set; }

    }
}
