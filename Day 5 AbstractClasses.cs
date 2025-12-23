using System;

public class Day5AbstractClasses
{
    // Abstract Methods
    abstract class Payment
    {
        public decimal Amount { get; }
        protected Payment(decimal amount) => Amount = amount;

        public void PrintReceipt()
        {
            Console.WriteLine($"Receipt: Amount={Amount}");
        }
        public abstract void Pay();
    }
    class UpiPayment : Payment
    {
        public string UpiId { get; set;}
        public UpiPayment(decimal amount, string upiId) : base(amount) => UpiId = upiId;
        public override void Pay()
        {
            Console.WriteLine($"Paid {Amount} via UPI ({UpiId}).");
        }
    }
    
    // Interface Methods
    interface IPrintable
    {
        void Print();
    }
    interface IExportable
    {
        void Export(string format);
    }

    class Report : IPrintable, IExportable
    {
        public string Title{ get; }
        public Report (string title) => Title = title;

        public void Print()
        {
            Console.WriteLine($"Printing report: {Title}");
        }
        public void Export(string format)
        {
            Console.WriteLine($"Exporting report: '{Title}' as {format}");
        }
    }

    public static void Run(string[] args)
    {
        var r = new Report("Sales Summary");
        r.Print();
        r.Export("PDF");
        Console.WriteLine("Done");
    }
}