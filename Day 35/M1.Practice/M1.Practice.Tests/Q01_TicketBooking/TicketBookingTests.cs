using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q01_TicketBooking;
using M1.Practice.Domain.Q01_TicketBooking;

namespace M1.Practice.Tests.Q01_TicketBooking
{
    [TestClass]
    public class TicketBookingTests
    {
        [TestMethod]
        public async Task OnlyOneUser_ShouldBookSeat()
        {
            // Arrange
            var seats = new List<Seat>
            {
                new Seat { SeatNo = 1 }
            };

            var service =
                new TicketBookingService(seats);

            bool result1 = false;
            bool result2 = false;

            // Act (Parallel booking)
            await Task.WhenAll(
                Task.Run(() =>
                    result1 = service.BookSeat(1, "U1")),
                Task.Run(() =>
                    result2 = service.BookSeat(1, "U2"))
            );

            // Assert
            Assert.IsTrue(result1 || result2);
            Assert.IsFalse(result1 && result2);
        }
    }
}
