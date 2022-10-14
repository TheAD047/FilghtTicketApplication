using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Flight
    {
        public int? flightID { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string? flightName { get; set; }

        [Required]
        public int airlineID { get; set; }

        //ref the airline of the flight
        public Airline? airline { get; set; }

        [Required]
        public DateTime departureDate { get; set; }

        [Required]
        public DateTime landingTime { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public String? departureFrom { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public String? arrivalAt { get; set; }

        //ref list of seats in a flight
        public List<Seat>? seats { get; set; }

    }
}
