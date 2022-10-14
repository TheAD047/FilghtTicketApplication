using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace FilghtTicketApplication.Models
{
    public class Seat
    {
        public int seatID { get; set; }

        [Required]
        public int flightID { get; set; }
        
        //Ref tbe flight to which the seat belongs to
        public Flight? Flight { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "One Row can only have 12 seats in total")]
        public int seatNum { get; set; }

        [Required]
        [MaxLength(1)]
        public string? seatRow { get; set; }

        [Required]
        public Boolean isBooked { get; set; }
    }
}
