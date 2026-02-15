using System;

public class CreditException : Exception
{
    public CreditException(string message) : base(message) { }
}

public static class CreditCard
{
    public static bool Valid(int age, string type, double income, double dues, int score, int defaults)
    {
        if(age<21 || age>65)
        {
            throw new CreditException("Invalid age");
        }
        if(type != "Salaried" && type!= "Self-Employed")
        {
            throw new CreditException("Invalid type");
        }
        if(income<20000)
        {
            throw new CreditException("Invalid income");
        }
        if(dues<0)
        {
            throw new CreditException("Invalid dues");
        }
        if(score<300 || score>900)
        {
            throw new CreditException("Invalid score");
        }
        if(defaults<0)
        {
            throw new CreditException("Invalid defaults");
        }
        return true;
    }

    public static double Calculate(double income, double dues, int score, int defaults)
    {
        double ratio = dues/(income*12);
        if(score<600 || defaults>=3 || ratio>0.4)
        {
            return 50000;
        }

        if(score>=750 && defaults==0 && ratio<0.25)
        {
            return 300000;
        }
        return 150000;
    }
}

public class Program
{
    public static void Main()
    {
        try
        {
            Console.Write("Enter name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter age: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new CreditException("Age input cannot be empty.");
            }
            int age = int.Parse(input);

            Console.Write("Enter type (Salaried/Self-Employed): ");
            string? typeInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typeInput))
            {
                throw new CreditException("Type input cannot be empty.");
            }
            string type = typeInput;

            Console.Write("Enter monthly income: ");
            string? incomeInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(incomeInput))
            {
                throw new CreditException("Income input cannot be empty.");
            }
            double income = double.Parse(incomeInput);

            Console.Write("Enter dues: ");
            string? duesInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(duesInput))
            {
                throw new CreditException("Dues input cannot be empty.");
            }
            double dues = double.Parse(duesInput);

            Console.Write("Enter score: ");
            string? scoreInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(scoreInput))
            {
                throw new CreditException("Score input cannot be empty.");
            }
            int creditScore = int.Parse(scoreInput);

            Console.Write("Enter loan defaults: ");
            string? defaultsInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(defaultsInput))
            {
                throw new CreditException("Loan defaults input cannot be empty.");
            }
            int defaults = int.Parse(defaultsInput);

            CreditCard.Valid(age, type, income, dues, creditScore, defaults);

            double limit = CreditCard.Calculate(income, dues, creditScore, defaults);

            Console.WriteLine($"\nCustomer Name: {name}");
            Console.WriteLine($"Approved Credit Limit: ₹{limit}");
        }
        catch(CreditException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input format");
        }
    }
}