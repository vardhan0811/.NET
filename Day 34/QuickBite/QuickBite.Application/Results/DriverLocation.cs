using System;

namespace QuickBite.Application.Results
{
    public class DriverLocation
    {
        public string DriverId { get; set; }

        public Location Location { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
