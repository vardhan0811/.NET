using CartService.Models;

namespace CartService.Services
{
    public class CartAp3
    {
        public CartDto Execute(CartDto cart)
        {
            Console.WriteLine("AP3: Calculating total");

            cart.TotalAmount = cart.Items.Sum(x => x.Price * x.Quantity);

            return cart;
        }
    }
}
