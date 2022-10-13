using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Ticket
    {
        public int ticketID { get; set; }

        [Required]
        public int flightID { get; set; }

        public Flight? flight { get; set; }

        [Required]
        public int seatID { get; set; }

        [Required]
        public DateTime purchasedOn { get; set; }

    }
}
