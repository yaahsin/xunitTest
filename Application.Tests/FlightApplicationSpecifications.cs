using Application.Models;
using Application.Services;
using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {

        readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);
        readonly BookingService bookingService;

        public FlightApplicationSpecifications()
        {
            bookingService = new BookingService(entities);
        }

        [Theory]
        [InlineData("a@email.com", 2)]
        [InlineData("b@email.com", 2)]
        public void Remembers_bookings(string email, int seats)
        {

            var flight = new Flight(3);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(flight.Id, email, seats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingRm(email, seats));
        }

        [Theory]
        [InlineData(3)]
        public void Frees_up_seats_after_booking(int initialCapacity)
        {

            // given
            var flight = new Flight(initialCapacity);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(flight.Id, "email@email.com", 2));

            // when
            bookingService.CancelBooking(
                new CancelBookingDto(Guid.NewGuid(), "email@email.com", 2)
                );
            // then
            bookingService.GetRemainingNumberOfSeatsFor(flight.Id).Should().Be(initialCapacity);
        }
    }
}