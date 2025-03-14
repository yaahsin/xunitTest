using Application.Tests;
using Data;

namespace Application
{
    public class BookingService
    {
        public Entities Entities { get; set; }

        public BookingService(Entities entities)
        {
            Entities = entities;
        }


        public void Book(BookDto bookDto)
        {
            var flight = Entities.Flights.Find(bookDto.FllightId);
            flight.Book(bookDto.email, bookDto.seats);
            Entities.SaveChanges();

        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return Entities.Flights
                .Find(flightId)
                .BookingList
                .Select(booking => new BookingRm(
                    booking.Email,
                    booking.NumberOfSeats
                    ));
        }

        public void CancelBooking(CancelBookingDto cancelBookingDto)
        {
            var flight = Entities.Flights.Find(cancelBookingDto.FlightId);
            flight.CancelBooking(cancelBookingDto.Email, cancelBookingDto.NumberOfSeats);
            Entities.SaveChanges();
        }

        public object GetRemainingNumberOfSeatsFor(Guid flightId)
        {
            return Entities.Flights.Find(flightId).RemainingNumberOfSeats;
        }
    }
}