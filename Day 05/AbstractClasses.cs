using System;

/// <summary>
/// Demonstrates abstract classes, interfaces, and implementation in C#.
/// </summary>
public class Day05AbstractClasses
{
    /// <summary>
    /// Abstract base class for payments.
    /// </summary>
    abstract class Payment
    {
        /// <summary>
        /// Gets the payment amount.
        /// </summary>
        public decimal Amount { get; }
        /// <summary>
        /// Initializes a new payment with the specified amount.
        /// </summary>
        /// <param name="amount">The payment amount.</param>
        protected Payment(decimal amount) => Amount = amount;

        /// <summary>
        /// Prints the payment receipt.
        /// </summary>
        public void PrintReceipt()
        {
            Console.WriteLine($"Receipt: Amount={Amount}");
        }
        /// <summary>
        /// Abstract method to perform payment.
        /// </summary>
        public abstract void Pay();
    }

    /// <summary>
    /// Represents a UPI payment, inheriting from Payment.
    /// </summary>
    class UpiPayment : Payment
    {
        /// <summary>
        /// Gets or sets the UPI ID.
        /// </summary>
        public string UpiId { get; set;}
        /// <summary>
        /// Initializes a new UPI payment.
        /// </summary>
        /// <param name="amount">The payment amount.</param>
        /// <param name="upiId">The UPI ID.</param>
        public UpiPayment(decimal amount, string upiId) : base(amount) => UpiId = upiId;
        /// <summary>
        /// Performs the UPI payment.
        /// </summary>
        public override void Pay()
        {
            Console.WriteLine($"Paid {Amount} via UPI ({UpiId}).");
        }
    }

    /// <summary>
    /// Interface for printable objects.
    /// </summary>
    interface IPrintable
    {
        /// <summary>
        /// Prints the object.
        /// </summary>
        void Print();
    }
    /// <summary>
    /// Interface for exportable objects.
    /// </summary>
    interface IExportable
    {
        /// <summary>
        /// Exports the object in the specified format.
        /// </summary>
        /// <param name="format">The export format.</param>
        void Export(string format);
    }

    /// <summary>
    /// Represents a report that can be printed and exported.
    /// </summary>
    class Report : IPrintable, IExportable
    {
        /// <summary>
        /// Gets the report title.
        /// </summary>
        public string Title{ get; }
        /// <summary>
        /// Initializes a new report with the specified title.
        /// </summary>
        /// <param name="title">The report title.</param>
        public Report (string title) => Title = title;

        /// <summary>
        /// Prints the report.
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"Printing report: {Title}");
        }
        /// <summary>
        /// Exports the report in the specified format.
        /// </summary>
        /// <param name="format">The export format.</param>
        public void Export(string format)
        {
            Console.WriteLine($"Exporting report: '{Title}' as {format}");
        }
    }

    /// <summary>
    /// Runs the demonstration of abstract classes and interfaces.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    public static void Run(string[] args)
    {
        // Create a report object
        var r = new Report("Sales Summary");
        // Print the report
        r.Print();
        // Export the report as PDF
        r.Export("PDF");
        // Indicate completion
        Console.WriteLine("Done");
    }
}