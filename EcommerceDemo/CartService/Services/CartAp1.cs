using CartService.Models;

namespace CartService.Services
{
    public class CartAp1
    {
        public void Execute(ProductDto product)
        {
            if (product.Price <= 0)
                throw new Exception("Invalid product");

            Console.WriteLine("AP1: Validated");
        }
    }
}
