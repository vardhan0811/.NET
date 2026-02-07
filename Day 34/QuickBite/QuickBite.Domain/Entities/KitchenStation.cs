namespace QuickBite.Domain.Entities
{
    public class KitchenStation
    {
        public string StationId { get; set; }

        public string StationType { get; set; } // Grill, Fryer, Salad, Dessert

        public int CurrentLoad { get; set; } // 0 - 100 %

        public int MaxCapacity { get; set; }

        public bool IsOperational { get; set; }

        public KitchenStation()
        {
            IsOperational = true;
            CurrentLoad = 0;
        }
    }
}
