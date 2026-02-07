namespace QuickBite.Application.Results
{
    public class AvailableDriver
    {
        public string DriverId { get; set; }

        public double Rating { get; set; }

        public int EstimatedArrivalMinutes { get; set; }

        public decimal Cost { get; set; }
    }
}
