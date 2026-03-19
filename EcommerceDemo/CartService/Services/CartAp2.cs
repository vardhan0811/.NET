using CartService.Models;

namespace CartService.Services
{
    public class CartAp2
    {
        public CartDto Execute(ProductDto product)
        {
            Console.WriteLine("AP2: Adding to cart");

            return new CartDto
            {
                CartId = Guid.NewGuid(),
                Items = new List<CartItemDto>
            {
                new CartItemDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                }
            },
                TotalAmount = product.Price
            };
        }
    }
}
