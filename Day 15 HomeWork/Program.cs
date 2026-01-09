using System;
namespace Day15HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample usage of the classes
            var person1 = new Person { Name = "Alice", Address = "123 Main St", Age = 30 }; // Create first person
            var person2 = new Person { Name = "Bob", Address = "456 Elm St", Age = 25 }; // Create second person
            var personList = new List<Person> { person1, person2 }; // Create list of persons

            // Person Implementation
            var personImpl = new PersonImplementation(); // Create instance of PersonImplementation
            personImpl.GetName(personList); // Display names and addresses
            Console.WriteLine($"Average Age: {personImpl.Average(personList)}"); // Display average age
            Console.WriteLine($"Max Age: {personImpl.Max(personList)}"); // Display maximum age

            // Method Overloading
            var source = new Source(); // Create instance of Source
            Console.WriteLine($"Add (int): {source.Add(1, 2, 3)}"); // Add three integers
            Console.WriteLine($"Add (double): {source.Add(1.1, 2.2, 3.3)}"); // Add three doubles

            var prepareBill = new PrepareBill(); // Create instance of PrepareBill
            prepareBill.SetTaxRates(CommodityCategory.Furniture, 12); // Set tax rate for Furniture
            prepareBill.SetTaxRates(CommodityCategory.Grocery, 5); // Set tax rate for Grocery
            var items = new List<Commodity>
            {
                new Commodity(CommodityCategory.Furniture, "Chair", 2, 100.0), // Create furniture item
                new Commodity(CommodityCategory.Grocery, "Apple", 5, 1.0) // Create grocery item
            };
            Console.WriteLine($"Total Bill: {prepareBill.CalculateBillAmount(items)}"); // Calculate total bill

            var blackPlan = new Black(true, 20); // Create instance of Black
            Console.WriteLine($"Black Plan Amount: {blackPlan.GetBroadbandPlanAmount()}"); // Get broadband plan amount
        }
    }
}