using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BookDto
    {
        public Guid FllightId { get; set; }
        public string email { get; set; }
        public int seats { get; set; }

        public BookDto(Guid flightId, string email, int seats)
        {
            FllightId = flightId;
            this.email = email;
            this.seats = seats;

        }
    }
}
