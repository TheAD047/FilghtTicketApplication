using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Ticket
    {
        public int ticketID { get; set; }

        [Required]
        public int flightID { get; set; }

        //ref the Flight
        public Flight? flight { get; set; }

        [Required]
        public int seatID { get; set; }

        //ref the Seat
        public Seat? seat { get; set; }

        [Required]
        public DateTime purchasedOn { get; set; }

    }
}
