using System;

namespace BookStoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO:
            // 1. Read initial input
            Console.WriteLine("Enter book details (BookID Title Price Stock): ");
            // Format: BookID Title Price Stock

            string[]? input = Console.ReadLine()?.Split(' ');

            Book book = new Book
            {
                Id = input?[0],
                Title = input?[1],
                Price = int.Parse(input?[2] ?? "0"),
                Stock = int.Parse(input?[3] ?? "0")
            };

            BookUtility utility = new BookUtility(book);

            Console.WriteLine("1. Display book details");
            Console.WriteLine("2. Update book price");
            Console.WriteLine("3. Update book stock");
            Console.WriteLine("4. Exit");

            while (true)
            {
                // TODO:
                // Display menu:
                // 1 -> Display book details
                // 2 -> Update book price
                // 3 -> Update book stock
                // 4 -> Exit
                Console.WriteLine("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine() ?? "0");

                switch (choice)
                {
                    case 1:
                        utility.GetBookDetails();
                        break;

                    case 2:
                        // TODO:
                        // Read new price
                        // Call UpdateBookPrice()
                        int newPrice = int.Parse(Console.ReadLine() ?? "0" );
                        utility.UpdateBookPrice(newPrice);
                        break;

                    case 3:
                        // TODO:
                        // Read new stock
                        // Call UpdateBookStock()
                        int newStock = int.Parse(Console.ReadLine() ?? "0");
                        utility.UpdateBookStock(newStock);
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        return;

                    default:
                        // TODO: Handle invalid choice
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
