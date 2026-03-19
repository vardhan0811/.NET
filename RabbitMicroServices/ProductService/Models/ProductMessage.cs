namespace ProductService.Models
{
    public class ProductMessage
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
