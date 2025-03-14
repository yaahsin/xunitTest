
namespace Application.Tests
{
    public class CancelBookingDto
    {
        public Guid FlightId { get; set; }
        public string Email { get; set; }
        public int NumberOfSeats { get; set; }

        public CancelBookingDto(Guid guid, string email, int numberOfSeats)
        {
            this.FlightId = guid;
            this.Email = email;
            this.NumberOfSeats = numberOfSeats;
        }
    }
}