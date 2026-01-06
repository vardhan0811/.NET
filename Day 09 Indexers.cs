using System;

/// <summary>
/// Demonstrates indexers, encapsulation, inheritance, interfaces, and OOP principles in C#.
/// </summary>
public class Student
{
    /// <summary>
    /// Gets or sets the roll number of the student.
    /// </summary>
    public int Rno { get; set; }
    /// <summary>
    /// Gets or sets the name of the student.
    /// </summary>
    public string? Name { get; set; }
    private string? address;

    private string[] books = new string[5];

    /// <summary>
    /// Indexer for accessing books by index.
    /// </summary>
    /// <param name="index">Book index.</param>
    /// <returns>Book title.</returns>
    public string this[int index]
    {
        get { return books[index]; }
        set { books[index] = value; }
    }

    /// <summary>
    /// Gets all books as an array.
    /// </summary>
    public string[] Books
    {
        get { return books; }
    }

    /// <summary>
    /// Gets or sets the address of the student.
    /// </summary>
    public string Address
    {
        get { return address ?? string.Empty; }
        set { address = value; }
    }
}

/// <summary>
/// Provides general utility methods.
/// </summary>
public static class GeneralUses
{
    static GeneralUses()
    {
        // Static constructor for initialization if needed
    }

    /// <summary>
    /// Prints a message indicating GetRno was called.
    /// </summary>
    public static void GetRno()
    {
        Console.WriteLine("GeneralUses.GetRno() called.");
    }
}

/// <summary>
/// Main class for Day 09 indexers and OOP exercises.
/// </summary>
public class Day09Indexers
{
    /// <summary>
    /// Runs the student and indexer demonstration.
    /// </summary>
    public static void Run()
    {
        // Create a student and set properties
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

        // Call utility method
        GeneralUses.GetRno();
    }


    /// <summary>
    /// Encapsulation – Bank Account Security
    /// </summary>
    public class BankAccount
    {
        private decimal balance;

        /// <summary>
        /// Gets the account balance.
        /// </summary>
        public decimal Balance
        {
            get { return balance; }
            private set { balance = value; }
        }

        /// <summary>
        /// Deposits an amount into the account.
        /// </summary>
        /// <param name="amount">Amount to deposit.</param>
        public void Deposit(decimal amount)
        {
            if (amount > 0)
                balance += amount;
        }

        /// <summary>
        /// Withdraws an amount from the account if sufficient balance.
        /// </summary>
        /// <param name="amount">Amount to withdraw.</param>
        /// <returns>True if withdrawal successful, false otherwise.</returns>
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

    /// <summary>
    /// Inheritance – Employee Payroll System
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee name.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Calculates the salary (virtual).
        /// </summary>
        /// <returns>Salary amount.</returns>
        public virtual decimal CalculateSalary() => 0;
    }

    /// <summary>
    /// Represents a full-time employee.
    /// </summary>
    public class FullTimeEmployee : Employee
    {
        /// <summary>
        /// Gets or sets the monthly salary.
        /// </summary>
        public decimal MonthlySalary { get; set; }
        /// <summary>
        /// Calculates the salary for full-time employee.
        /// </summary>
        /// <returns>Monthly salary.</returns>
        public override decimal CalculateSalary() => MonthlySalary;
    }

    /// <summary>
    /// Represents a contract employee.
    /// </summary>
    public class ContractEmployee : Employee
    {
        /// <summary>
        /// Gets or sets the hourly rate.
        /// </summary>
        public decimal HourlyRate { get; set; }
        /// <summary>
        /// Gets or sets the hours worked.
        /// </summary>
        public int HoursWorked { get; set; }
        /// <summary>
        /// Calculates the salary for contract employee.
        /// </summary>
        /// <returns>Salary based on hours worked.</returns>
        public override decimal CalculateSalary() => HourlyRate * HoursWorked;
    }

    /// <summary>
    /// Polymorphism – Online Payment Gateway
    /// </summary>
    public abstract class PaymentMethod
    {
        /// <summary>
        /// Processes a payment of the specified amount.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        public abstract void ProcessPayment(decimal amount);
    }

    /// <summary>
    /// Credit card payment method.
    /// </summary>
    public class CreditCard : PaymentMethod
    {
        /// <summary>
        /// Processes a credit card payment.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Credit Card payment of {amount:C} processed.");
        }
    }

    /// <summary>
    /// UPI payment method.
    /// </summary>
    public class UPI : PaymentMethod
    {
        /// <summary>
        /// Processes a UPI payment.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"UPI payment of {amount:C} processed.");
        }
    }

    /// <summary>
    /// NetBanking payment method.
    /// </summary>
    public class NetBanking : PaymentMethod
    {
        /// <summary>
        /// Processes a NetBanking payment.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"NetBanking payment of {amount:C} processed.");
        }
    }

    /// <summary>
    /// Abstraction – Vehicle Rental System
    /// </summary>
    public abstract class Vehicle
    {
        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string? Model { get; set; }
        /// <summary>
        /// Calculates the rental price for the given number of days.
        /// </summary>
        /// <param name="days">Number of days.</param>
        /// <returns>Rental price.</returns>
        public abstract decimal CalculateRentalPrice(int days);
    }

    /// <summary>
    /// Car vehicle type.
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Calculates rental price for car.
        /// </summary>
        /// <param name="days">Number of days.</param>
        /// <returns>Rental price.</returns>
        public override decimal CalculateRentalPrice(int days) => days * 1000;
    }

    /// <summary>
    /// Bike vehicle type.
    /// </summary>
    public class Bike : Vehicle
    {
        /// <summary>
        /// Calculates rental price for bike.
        /// </summary>
        /// <param name="days">Number of days.</param>
        /// <returns>Rental price.</returns>
        public override decimal CalculateRentalPrice(int days) => days * 300;
    }

    /// <summary>
    /// Truck vehicle type.
    /// </summary>
    public class Truck : Vehicle
    {
        /// <summary>
        /// Calculates rental price for truck.
        /// </summary>
        /// <param name="days">Number of days.</param>
        /// <returns>Rental price.</returns>
        public override decimal CalculateRentalPrice(int days) => days * 2000;
    }

    /// <summary>
    /// Interface – Notification System
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// Sends a notification message.
        /// </summary>
        /// <param name="to">Recipient.</param>
        /// <param name="message">Message content.</param>
        void Send(string to, string message);
    }

    /// <summary>
    /// Email notification implementation.
    /// </summary>
    public class EmailNotification : INotification
    {
        /// <summary>
        /// Sends an email notification.
        /// </summary>
        /// <param name="to">Recipient.</param>
        /// <param name="message">Message content.</param>
        public void Send(string to, string message)
        {
            Console.WriteLine($"Email sent to {to}: {message}");
        }
    }

    /// <summary>
    /// SMS notification implementation.
    /// </summary>
    public class SmsNotification : INotification
    {
        /// <summary>
        /// Sends an SMS notification.
        /// </summary>
        /// <param name="to">Recipient.</param>
        /// <param name="message">Message content.</param>
        public void Send(string to, string message)
        {
            Console.WriteLine($"SMS sent to {to}: {message}");
        }
    }

    /// <summary>
    /// WhatsApp notification implementation.
    /// </summary>
    public class WhatsAppNotification : INotification
    {
        /// <summary>
        /// Sends a WhatsApp notification.
        /// </summary>
        /// <param name="to">Recipient.</param>
        /// <param name="message">Message content.</param>
        public void Send(string to, string message)
        {
            Console.WriteLine($"WhatsApp message sent to {to}: {message}");
        }
    }

    /// <summary>
    /// Method Overloading – Billing System
    /// </summary>
    public class Billing
    {
        /// <summary>
        /// Calculates total price.
        /// </summary>
        /// <param name="price">Base price.</param>
        /// <returns>Total price.</returns>
        public decimal CalculateTotal(decimal price)
        {
            return price;
        }

        /// <summary>
        /// Calculates total price with tax.
        /// </summary>
        /// <param name="price">Base price.</param>
        /// <param name="tax">Tax amount.</param>
        /// <returns>Total price.</returns>
        public decimal CalculateTotal(decimal price, decimal tax)
        {
            return price + tax;
        }

        /// <summary>
        /// Calculates total price with tax and discount.
        /// </summary>
        /// <param name="price">Base price.</param>
        /// <param name="tax">Tax amount.</param>
        /// <param name="discount">Discount amount.</param>
        /// <returns>Total price.</returns>
        public decimal CalculateTotal(decimal price, decimal tax, decimal discount)
        {
            return price + tax - discount;
        }
    }

    /// <summary>
    /// Method Overriding – Insurance Premium Calculation
    /// </summary>
    public abstract class Insurance
    {
        /// <summary>
        /// Gets or sets the policy holder name.
        /// </summary>
        public string? PolicyHolder { get; set; }
        /// <summary>
        /// Calculates the insurance premium.
        /// </summary>
        /// <returns>Premium amount.</returns>
        public abstract decimal CalculatePremium();
    }

    /// <summary>
    /// Health insurance implementation.
    /// </summary>
    public class HealthInsurance : Insurance
    {
        /// <summary>
        /// Calculates health insurance premium.
        /// </summary>
        /// <returns>Premium amount.</returns>
        public override decimal CalculatePremium() => 5000;
    }

    /// <summary>
    /// Life insurance implementation.
    /// </summary>
    public class LifeInsurance : Insurance
    {
        /// <summary>
        /// Calculates life insurance premium.
        /// </summary>
        /// <returns>Premium amount.</returns>
        public override decimal CalculatePremium() => 8000;
    }

    /// <summary>
    /// Vehicle insurance implementation.
    /// </summary>
    public class VehicleInsurance : Insurance
    {
        /// <summary>
        /// Calculates vehicle insurance premium.
        /// </summary>
        /// <returns>Premium amount.</returns>
        public override decimal CalculatePremium() => 3000;
    }

    /// <summary>
    /// Composition – Order and Product Relationship
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Represents an order containing products.
    /// </summary>
    public class Order
    {
        private List<Product> products = new List<Product>();

        /// <summary>
        /// Adds a product to the order.
        /// </summary>
        /// <param name="product">Product to add.</param>
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        /// <summary>
        /// Gets the total price of all products in the order.
        /// </summary>
        /// <returns>Total price.</returns>
        public decimal GetTotal()
        {
            return products.Sum(p => p.Price);
        }

        /// <summary>
        /// Gets all products in the order.
        /// </summary>
        public IEnumerable<Product> Products => products;
    }

    /// <summary>
    /// Interface vs Abstract Class – Reporting Module
    /// </summary>
    /// <remarks>
    /// Use interface because PDF, Excel, and CSV reporting share only the contract (GenerateReport), not implementation or state.
    /// </remarks>
    public interface IReportGenerator
    {
        /// <summary>
        /// Generates a report from the given data.
        /// </summary>
        /// <param name="data">Data to report.</param>
        void GenerateReport(string data);
    }

    /// <summary>
    /// PDF report generator implementation.
    /// </summary>
    public class PdfReport : IReportGenerator
    {
        /// <summary>
        /// Generates a PDF report.
        /// </summary>
        /// <param name="data">Data to report.</param>
        public void GenerateReport(string data)
        {
            Console.WriteLine("PDF Report generated.");
        }
    }

    /// <summary>
    /// Excel report generator implementation.
    /// </summary>
    public class ExcelReport : IReportGenerator
    {
        /// <summary>
        /// Generates an Excel report.
        /// </summary>
        /// <param name="data">Data to report.</param>
        public void GenerateReport(string data)
        {
            Console.WriteLine("Excel Report generated.");
        }
    }

    /// <summary>
    /// CSV report generator implementation.
    /// </summary>
    public class CsvReport : IReportGenerator
    {
        /// <summary>
        /// Generates a CSV report.
        /// </summary>
        /// <param name="data">Data to report.</param>
        public void GenerateReport(string data)
        {
            Console.WriteLine("CSV Report generated.");
        }
    }

    /// <summary>
    /// SOLID + OOPS – E-Commerce Order Processing
    /// </summary>
    /// <remarks>
    /// S: Each class has a single responsibility.
    /// O: Open for extension, closed for modification.
    /// L: Derived classes can substitute base classes.
    /// I: Interfaces are segregated.
    /// D: High-level modules depend on abstractions.
    /// </remarks>
    public interface IPaymentProcessor
    {
        /// <summary>
        /// Processes a payment of the specified amount.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        void ProcessPayment(decimal amount);
    }

    /// <summary>
    /// Card payment processor implementation.
    /// </summary>
    public class CardPaymentProcessor : IPaymentProcessor
    {
        /// <summary>
        /// Processes a card payment.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Card payment of {amount:C} processed.");
        }
    }

    /// <summary>
    /// Cash payment processor implementation.
    /// </summary>
    public class CashPaymentProcessor : IPaymentProcessor
    {
        /// <summary>
        /// Processes a cash payment.
        /// </summary>
        /// <param name="amount">Amount to process.</param>
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Cash payment of {amount:C} processed.");
        }
    }

    /// <summary>
    /// Represents an e-commerce order with products and payment processing.
    /// </summary>
    public class ECommerceOrder
    {
        private readonly IPaymentProcessor paymentProcessor;
        private readonly List<Product> products = new List<Product>();

        /// <summary>
        /// Initializes a new e-commerce order with a payment processor.
        /// </summary>
        /// <param name="paymentProcessor">Payment processor.</param>
        public ECommerceOrder(IPaymentProcessor paymentProcessor)
        {
            this.paymentProcessor = paymentProcessor;
        }

        /// <summary>
        /// Adds a product to the order.
        /// </summary>
        /// <param name="product">Product to add.</param>
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        /// <summary>
        /// Gets the total price of the order.
        /// </summary>
        /// <returns>Total price.</returns>
        public decimal GetOrderTotal()
        {
            return products.Sum(p => p.Price);
        }

        /// <summary>
        /// Processes the order checkout and payment.
        /// </summary>
        public void Checkout()
        {
            decimal total = GetOrderTotal();
            paymentProcessor.ProcessPayment(total);
            Console.WriteLine("Order processed successfully.");
        }
    }
}