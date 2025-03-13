using Domain;
using FluentAssertions;

namespace FlightTests
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3, 1, 2)]
        [InlineData(6, 3, 3)]
        [InlineData(10, 6, 4)]
        public void Booking_reduces_the_number_of_seat(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
        {
            var flight = new Flight(seatCapacity: seatCapacity);
            flight.Book("someemail@email.com", numberOfSeats);

            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            //given
            var flight = new Flight(seatCapacity: 3);

            // when
            var error = flight.Book("john@email.com", 4);

            // then
            error.Should().BeOfType<OverbookingError>();

        }

        [Fact]
        public void Booking_should_successfully()
        {
            //given
            var flight = new Flight(seatCapacity: 3);

            // when
            var error = flight.Book("jenny@email.com", 1);

            // then
            error.Should().BeNull();

        }

        [Fact]

        public void Remenber_bookings()
        {

            var flight = new Flight(seatCapacity: 1590);

            flight.Book("passenger@email.com", 4);

            flight.BookingList.Should().ContainEquivalentOf(new Booking("passenger@email.com", 4));
        }

        [Theory]
        [InlineData(3, 1, 1, 3)]
        [InlineData(4, 2, 2, 4)]
        [InlineData(7, 5, 4, 6)]
        public void Canceling_bookings_frees_up_the_seats(int initialCapacity, int numberOfSeatsToBook,
            int numberOfSeatsToCancel, int remainingNumberOfSeats)
        {

            // given
            var flight = new Flight(initialCapacity);
            flight.Book("passenger@email.com", numberOfSeatsToBook);
            flight.CancelBooking("passenger@email.com", numberOfSeatsToCancel);

            // then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Doesnt_cancel_booking_for_passenger_who_have_not_booked() {

            var flight = new Flight(3);
            var error = flight.CancelBooking("email@email.com", 2);

            error.Should().BeOfType<BookingNotFoundError>();
        }

        [Fact]
        public void Return_null_when_successfully_cancels_a_booking()
        {

            var flight = new Flight(3);
            flight.Book("email@email.com", 2);
            var error = flight.CancelBooking("email@email.com", 1);

            error.Should().BeNull();
        }
    }
}