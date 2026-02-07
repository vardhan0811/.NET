namespace QuickBite.Domain.Entities
{
    public class OrderItem
    {
        public string ItemId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; } // Grill, Fryer, Salad, Dessert
    }
}
