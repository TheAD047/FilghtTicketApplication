using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Airline
    {
        public int airlineID { get; set; }

        [Required]
        public string? airlineName { get; set; }

        [Required]
        public string? airlineEmail { get; set; }

        public override string ToString()
        {
            return airlineName;
        }
    }
}
