using QuickBite.Domain.Entities;

namespace QuickBite.Application.Results
{
    public class DriverAssignment
    {
        public bool IsAccepted { get; set; }

        public Driver Driver { get; set; }
    }
}
