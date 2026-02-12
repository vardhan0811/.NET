using System;

namespace Day38
{    
    abstract class DiscountPolicy
    {
        public abstract double GetFinalAmount(double amount);
    }

    class FestivalDiscount : DiscountPolicy
    {
        public override double GetFinalAmount(double amount)
        {
            if (amount >= 5000)
                return amount * 0.90; // 10% off
            else
                return amount * 0.95; // 5% off
        }
    }

    class MemberDiscount : DiscountPolicy
    {
        public override double GetFinalAmount(double amount)
        {
            if (amount >= 2000)
                return amount - 300; // flat 300 off
            else
                return amount;
        }
    }

    class ECommerce
    {
        public static void Run()
        {
            Console.WriteLine("Enter purchase amount:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !double.TryParse(input, out double amount))
            {
                Console.WriteLine("Invalid amount entered.");
                return;
            }

            Console.WriteLine("Choose Discount Policy: 1. Festival  2. Member");
            string? choiceInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(choiceInput) || !int.TryParse(choiceInput, out int choice))
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            DiscountPolicy policy;

            if (choice == 1)
                policy = new FestivalDiscount();
            else if (choice == 2)
                policy = new MemberDiscount();
            else
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            double finalAmount = policy.GetFinalAmount(amount);
            Console.WriteLine($"Final payable amount: {finalAmount:F2}");
        }
    }
}