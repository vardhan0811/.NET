using CartService.Models;

namespace CartService.Services
{
    public class Calculate
    {
        public CartMessage Execute(CartMessage cart)
        {
            Console.WriteLine("AP3: Calculating total");

            cart.TotalAmount = cart.Items.Sum(x => x.Price * x.Quantity);

            return cart;
        }
    }
}
