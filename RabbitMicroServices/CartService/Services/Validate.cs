using CartService.Models;

namespace CartService.Services
{
    public class Validate
    {
        public void Execute(ProductMessage msg)
        {
            if (msg.Price <= 0)
                throw new Exception("Invalid price");

            Console.WriteLine("AP1: Validation done");
        }
    }
}
