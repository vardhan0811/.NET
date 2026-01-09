using System;

public delegate string PrintMessage(string message);

public class PrintingCompany
{
    public PrintMessage? CustomerChoicePrintMessage { get; set; }

    public void Print(string message)
    {
        if (CustomerChoicePrintMessage != null)
        {
            string messageToPrint = CustomerChoicePrintMessage(message);
            Console.WriteLine(messageToPrint);
        }
        else
        {
            Console.WriteLine("No print message delegate assigned.");
        }
    }
}

public class Day15Delegates
{
    public static void Run(string[] args)
    {
        PrintingCompany printingCompany = new PrintingCompany();
        printingCompany.CustomerChoicePrintMessage = new PrintMessage(Method1);
        printingCompany.Print("Hello, Delegates!");
    }

    private static string Method1(string message)
    {
        return $"Welcome to the delegate method: {message}";
    }
}