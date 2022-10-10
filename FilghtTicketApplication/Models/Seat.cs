using System.ComponentModel.DataAnnotations;

namespace FilghtTicketApplication.Models
{
    public class Seat
    {
        public int seatID { get; set; }

        [Required]
        public int flightID { get; set; }

        [Required]
        public int seatNum { get; set; }

        [Required]
        public string? seatRow { get; set; }

    }
}
