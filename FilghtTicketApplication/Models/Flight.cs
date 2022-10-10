using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Flight
    {
        public int? flightID { get; set; }

        [Required]
        public string? airlineName { get; set; }

        [Required]
        public DateTime departureDate { get; set; }

        [Required]
        public DateTime landingTime { get; set; }

        [Required]
        public String? departureFrom { get; set; }

        [Required]
        public String? arrivalAt { get; set; }

        //ref list of seats in a flight
        public List<Seat>? seats { get; set; }
    }
}
