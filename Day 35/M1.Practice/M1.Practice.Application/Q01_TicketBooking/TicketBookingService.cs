using System.Collections.Generic;
using System.Linq;
using M1.Practice.Domain.Q01_TicketBooking;

namespace M1.Practice.Application.Q01_TicketBooking
{
    public class TicketBookingService
    {
        private readonly object _lock = new object();

        private readonly List<Seat> _seats;

        public TicketBookingService(List<Seat> seats)
        {
            _seats = seats;
        }

        // Thread-safe booking
        public bool BookSeat(int seatNo, string userId)
        {
            lock (_lock)
            {
                var seat = _seats
                    .FirstOrDefault(s => s.SeatNo == seatNo);

                if (seat == null)
                    return false;

                if (seat.IsBooked)
                    return false;

                seat.IsBooked = true;
                seat.BookedBy = userId;

                return true;
            }
        }
    }
}
