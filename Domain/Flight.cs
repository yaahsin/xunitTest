namespace Domain
{
    public class Flight
    {
        public Guid Id { get; }
        public int RemainingNumberOfSeats { get; set; }
        public IEnumerable<Booking> BookingList => bookingList;

        List<Booking> bookingList = new();


        [Obsolete("Needed by EF")]
        Flight() { }

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public Object? Book(string passengerEmail, int seat)
        {
            if (seat > this.RemainingNumberOfSeats)
            {
                return new OverbookingError();
            }

            RemainingNumberOfSeats -= seat;

            bookingList.Add(new Booking(passengerEmail, seat));

            return null;
        }

        public Object? CancelBooking(string email, int seats)
        {
            RemainingNumberOfSeats += seats;

            if (!bookingList.Any(booking => booking.Email == email))
            {
                return new BookingNotFoundError();
            }
            return null;
        }
    }
}