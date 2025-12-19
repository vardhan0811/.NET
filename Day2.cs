using System;
public class Day2{
    public static void Run(string[] args)
    {

        // 1. Height classification 
        Console.Write("Enter your height in cm: ");
        if (int.TryParse(Console.ReadLine(), out int height))
        {
            if (height < 150)
                Console.WriteLine("Dwarf");
            else if (height <= 165)
                Console.WriteLine("Average");
            else if (height <= 190)
                Console.WriteLine("Tall");
            else
                Console.WriteLine("Abnormal");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }


        // 2. Largest of Three Numbers
        Console.Write("Enter first number: ");
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter second number: ");
        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter third number: ");
        if (!int.TryParse(Console.ReadLine(), out int c))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        int max = a;
        if (b > max) max = b;
        if (c > max) max = c;
        Console.WriteLine("The largest number is: " + max);


        // 3. Leap Year Check
        Console.Write("Enter a year: ");
        if (int.TryParse(Console.ReadLine(), out int year))
        {
            if ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0))
                Console.WriteLine(year + " is a leap year.");
            else
                Console.WriteLine(year + " is not a leap year.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }


        // 4. Admission Eligibility
        Console.Write("Enter marks in Mathematics: ");
        if (!int.TryParse(Console.ReadLine(), out int math))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter marks in Physics: ");
        if (!int.TryParse(Console.ReadLine(), out int phys))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter marks in Chemistry: ");
        if (!int.TryParse(Console.ReadLine(), out int chem))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        int total = math + phys + chem;
        if (math >= 65 && phys >= 55 && chem >= 50 && (total >= 180 || (math + phys) >= 140))
            Console.WriteLine("The candidate is eligible for admission.");
        else
            Console.WriteLine("The candidate is not eligible for admission.");


        // 5. Even or Odd Check
        int number;
        while (true)
        {
            Console.Write("Enter a non-negative integer: ");
            if (int.TryParse(Console.ReadLine(), out number) && number >= 0)
                break;
            Console.WriteLine("Invalid input. Please enter a non-negative integer.");
        }
        if (number % 2 == 0)
            Console.WriteLine(number + " is even.");
        else
            Console.WriteLine(number + " is odd.");


        // 6. Vowel or Consonant Check 
        Console.WriteLine("Enter a single alphabet character: ");
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input) && char.IsLetter(input[0]))
        {
            char ch = char.ToLower(input[0]);
            if ("aeiou".Contains(ch))
            {
                Console.WriteLine(ch + " is a vowel.");
            }
            else
            {
                Console.WriteLine(ch + " is a consonant.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 7. Division with Exception Handling
        int da, db;
        Console.WriteLine("Enter a number: ");
        while (!int.TryParse(Console.ReadLine(), out da))
        {
            Console.WriteLine("Enter valid input: ");
        }
        Console.WriteLine("Enter another number: ");
        while (!int.TryParse(Console.ReadLine(), out db))
        {
            Console.WriteLine("Enter valid input: ");
        }
        try
        {
            int result = da / db;
            Console.WriteLine("Result of division: " + result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero.");
        }


        // 8. Triangle Type Check
        Console.Write("Enter side 1 of the triangle: ");
        if (!int.TryParse(Console.ReadLine(), out int side1))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter side 2 of the triangle: ");
        if (!int.TryParse(Console.ReadLine(), out int side2))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter side 3 of the triangle: ");
        if (!int.TryParse(Console.ReadLine(), out int side3))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        // Check for triangle validity
        if (side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1)
        {
            if (side1 == side2 && side2 == side3)
            Console.WriteLine("Equilateral triangle.");
            else if (side1 == side2 || side2 == side3 || side1 == side3)
            Console.WriteLine("Isosceles triangle.");
            else
            Console.WriteLine("Scalene triangle.");
        }
        else
        {
            Console.WriteLine("The given sides do not form a valid triangle.");
        }
    
    }

}