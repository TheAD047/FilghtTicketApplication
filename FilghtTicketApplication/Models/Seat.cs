namespace FilghtTicketApplication.Models
{
    public class Seat
    {
        public int seatID { get; set; }

        public int flightID { get; set; }

        public Flight? Flight { get; set; }

        public int seatNum { get; set; }

        public String? seatRow { get; set; }

        public Boolean isBooked { get; set; }
    }
}
