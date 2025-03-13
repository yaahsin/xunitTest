using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("a@email.com", 2)]
        [InlineData("b@email.com", 2)]
        public void Booksw_flights(string email, int seats)
        {

            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);
            var flight = new Flight(3);
            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities);
            bookingService.Book(new BookDto(flight.Id, email, seats));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(new BookingRm(email, seats));
        }
    }

    public class BookingService
    {
        public Entities Entities { get; set; }

        public BookingService(Entities entities)
        {
            this.Entities = entities;
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
    }

    public class BookDto
    {
        public Guid FllightId { get; set; }
        public string email { get; set; }
        public int seats { get; set; }

        public BookDto(Guid flightId, string email, int seats)
        {
            this.FllightId = flightId;
            this.email = email;
            this.seats = seats;

        }
    }

    public class BookingRm
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }

        public BookingRm(string email, int seats)
        {
            this.PassengerEmail = email;
            this.NumberOfSeats = seats;
        }
    }



}