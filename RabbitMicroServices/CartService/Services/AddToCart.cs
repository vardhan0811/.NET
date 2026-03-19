using CartService.Models;

namespace CartService.Services
{
    public class AddToCart
    {
        public CartMessage Execute(ProductMessage msg)
        {
            Console.WriteLine("AP2: Adding to cart");

            var cart = new CartMessage
            {
                CartId = Guid.NewGuid(),
                Items = new List<CartItem>
            {
                new CartItem
                {
                    ProductId = msg.ProductId,
                    Name = msg.Name,
                    Price = msg.Price,
                    Quantity = 1
                }
            },
                TotalAmount = msg.Price
            };

            return cart;
        }
    }
}
