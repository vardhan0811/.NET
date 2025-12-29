using System;

public class Student
{
    public int Rno { get; set; }
    public string? Name { get; set; }
    private string? address;

    private string[] books = new string[5];

    // Indexer for books
    public string this[int index]
    {
        get { return books[index]; }
        set { books[index] = value; }
    }

    // Property to get all books as an array
    public string[] Books
    {
        get { return books; }
    }

    // Optional: Property for address (if you want to set/get it)
    public string Address
    {
        get { return address ?? string.Empty; }
        set { address = value; }
    }
}



public static class GeneralUses
{
    static GeneralUses()
    {
        
    }

    public static void GetRno()
    {
        Console.WriteLine("GeneralUses.GetRno() called.");
    }
}

public class Day9Indexers
{
    public static void Run()
    {
        Student data = new Student();
        data.Rno = 36;
        data.Name = "Vardhan Rayapureddy";
        data.Address = "Hyderabad";

        // Using the indexer to set book titles
        data[0] = "Quantum Mechanics";
        data[1] = "Astrology";
        data[2] = "Physiology";

        // Displaying student information
        Console.WriteLine($"Roll No: {data.Rno}");
        Console.WriteLine($"Name: {data.Name}");
        Console.WriteLine($"Address: {data.Address}");
        Console.WriteLine("Books:");
        foreach (var book in data.Books)
        {
            if (!string.IsNullOrEmpty(book))
            {
                Console.WriteLine($" - {book}");
            }
        }

        GeneralUses.GetRno();
    }


    // Question 1: Encapsulation – Bank Account Security
    public class BankAccount
    {
        private decimal balance;

        public decimal Balance
        {
            get { return balance; }
            private set { balance = value; }
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
                balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                return true;
            }
            return false;
        }
    }

    // Question 2: Inheritance – Employee Payroll System
    public class Employee
    {
        public string Name { get; set; }
        public virtual decimal CalculateSalary() => 0;
    }

    public class FullTimeEmployee : Employee
    {
        public decimal MonthlySalary { get; set; }
        public override decimal CalculateSalary() => MonthlySalary;
    }

    public class ContractEmployee : Employee
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public override decimal CalculateSalary() => HourlyRate * HoursWorked;
    }

    // Question 3: Polymorphism – Online Payment Gateway
    public abstract class PaymentMethod
    {
        public abstract void ProcessPayment(decimal amount);
    }

    public class CreditCard : PaymentMethod
    {
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Credit Card payment of {amount:C} processed.");
        }
    }

    public class UPI : PaymentMethod
    {
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"UPI payment of {amount:C} processed.");
        }
    }

    public class NetBanking : PaymentMethod
    {
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"NetBanking payment of {amount:C} processed.");
        }
    }

    // Question 4: Abstraction – Vehicle Rental System
    public abstract class Vehicle
    {
        public string Model { get; set; }
        public abstract decimal CalculateRentalPrice(int days);
    }

    public class Car : Vehicle
    {
        public override decimal CalculateRentalPrice(int days) => days * 1000;
    }

    public class Bike : Vehicle
    {
        public override decimal CalculateRentalPrice(int days) => days * 300;
    }

    public class Truck : Vehicle
    {
        public override decimal CalculateRentalPrice(int days) => days * 2000;
    }

    // Question 5: Interface – Notification System
    public interface INotification
    {
        void Send(string to, string message);
    }

    public class EmailNotification : INotification
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Email sent to {to}: {message}");
        }
    }

    public class SmsNotification : INotification
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"SMS sent to {to}: {message}");
        }
    }

    public class WhatsAppNotification : INotification
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"WhatsApp message sent to {to}: {message}");
        }
    }

    // Question 6: Method Overloading – Billing System
    public class Billing
    {
        public decimal CalculateTotal(decimal price)
        {
            return price;
        }

        public decimal CalculateTotal(decimal price, decimal tax)
        {
            return price + tax;
        }

        public decimal CalculateTotal(decimal price, decimal tax, decimal discount)
        {
            return price + tax - discount;
        }
    }

    // Question 7: Method Overriding – Insurance Premium Calculation
    public abstract class Insurance
    {
        public string PolicyHolder { get; set; }
        public abstract decimal CalculatePremium();
    }

    public class HealthInsurance : Insurance
    {
        public override decimal CalculatePremium() => 5000;
    }

    public class LifeInsurance : Insurance
    {
        public override decimal CalculatePremium() => 8000;
    }

    public class VehicleInsurance : Insurance
    {
        public override decimal CalculatePremium() => 3000;
    }

    // Question 8: Composition – Order and Product Relationship
    // Explanation: Composition means an Order "has-a" collection of Products, not "is-a" Product.
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public decimal GetTotal()
        {
            return products.Sum(p => p.Price);
        }

        public IEnumerable<Product> Products => products;
    }

    // Question 9: Interface vs Abstract Class – Reporting Module
    // Justification: Use interface because PDF, Excel, and CSV reporting share only the contract (GenerateReport), not implementation or state.
    public interface IReportGenerator
    {
        void GenerateReport(string data);
    }

    public class PdfReport : IReportGenerator
    {
        public void GenerateReport(string data)
        {
            Console.WriteLine("PDF Report generated.");
        }
    }

    public class ExcelReport : IReportGenerator
    {
        public void GenerateReport(string data)
        {
            Console.WriteLine("Excel Report generated.");
        }
    }

    public class CsvReport : IReportGenerator
    {
        public void GenerateReport(string data)
        {
            Console.WriteLine("CSV Report generated.");
        }
    }

    // Question 10: SOLID + OOPS – E-Commerce Order Processing
    // S: Each class has a single responsibility.
    // O: Open for extension, closed for modification.
    // L: Derived classes can substitute base classes.
    // I: Interfaces are segregated.
    // D: High-level modules depend on abstractions.

    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
    }

    public class CardPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Card payment of {amount:C} processed.");
        }
    }

    public class CashPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Cash payment of {amount:C} processed.");
        }
    }

    public class ECommerceOrder
    {
        private readonly IPaymentProcessor paymentProcessor;
        private readonly List<Product> products = new List<Product>();

        public ECommerceOrder(IPaymentProcessor paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public decimal GetOrderTotal()
        {
            return products.Sum(p => p.Price);
        }

        public void Checkout()
        {
            decimal total = GetOrderTotal();
            paymentProcessor.ProcessPayment(total);
            Console.WriteLine("Order processed successfully.");
        }
    }
}