using System;
using System.Collections.Generic;
using System.Text;

namespace M1.Practice.Domain.Q01_TicketBooking
{
    public class Seat
    {
        public int SeatNo { get; set; }

        public bool IsBooked { get; set; }

        public string BookedBy { get; set; }
    }
}
