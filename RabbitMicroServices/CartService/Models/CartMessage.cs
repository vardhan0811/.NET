namespace CartService.Models
{
    public class CartMessage
    {
        public Guid CartId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
