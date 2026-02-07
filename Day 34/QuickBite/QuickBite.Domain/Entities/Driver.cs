namespace QuickBite.Domain.Entities
{
    public class Driver
    {
        public string DriverId { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public bool IsAvailable { get; set; }

        public string VehicleNumber { get; set; }
    }
}
