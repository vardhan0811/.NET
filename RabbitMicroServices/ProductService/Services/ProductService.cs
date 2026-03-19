using ProductService.Models;

namespace ProductService.Services
{
    public class ProductServices
    {
        public ProductMessage GetProduct(int id)
        {
            return new ProductMessage
            {
                ProductId = id,
                Name = "Laptop",
                Price = 50000
            };
        }
    }
}
