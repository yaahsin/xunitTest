namespace Application.Models
{
    public class BookingRm
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }

        public BookingRm(string email, int seats)
        {
            PassengerEmail = email;
            NumberOfSeats = seats;
        }
    }
}
