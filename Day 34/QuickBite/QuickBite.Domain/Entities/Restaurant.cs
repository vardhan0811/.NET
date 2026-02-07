using System.Collections.Generic;

namespace QuickBite.Domain.Entities
{
    public class Restaurant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public bool IsOnline { get; set; }

        public int PreparationTimeMinutes { get; set; }

        public List<KitchenStation> KitchenStations { get; set; }

        public Restaurant()
        {
            KitchenStations = new List<KitchenStation>();
            IsOnline = true;
        }
    }
}
