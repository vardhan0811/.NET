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

        // 9. Point Quadrant Check
        Console.Write("Enter x coordinate: ");
        if (!int.TryParse(Console.ReadLine(), out int x))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter y coordinate: ");
        if (!int.TryParse(Console.ReadLine(), out int y))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        if (x == 0 && y == 0)
            Console.WriteLine("Point is at the origin.");
        else if (x == 0)
            Console.WriteLine("Point lies on the Y axis.");
        else if (y == 0)
            Console.WriteLine("Point lies on the X axis.");
        else if (x > 0 && y > 0)
            Console.WriteLine("Point is in Quadrant I.");
        else if (x < 0 && y > 0)
            Console.WriteLine("Point is in Quadrant II.");
        else if (x < 0 && y < 0)
            Console.WriteLine("Point is in Quadrant III.");
        else
            Console.WriteLine("Point is in Quadrant IV.");
    

        // 10. Grade Description using switch 
        Console.Write("Enter grade (E, V, G, A, F): ");
        string? gradeInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(gradeInput) && gradeInput.Length == 1)
        {
            char grade = char.ToUpper(gradeInput[0]);
            switch (grade)
            {
                case 'E':
                    Console.WriteLine("Excellent");
                    break;
                case 'V':
                    Console.WriteLine("Very Good");
                    break;
                case 'G':
                    Console.WriteLine("Good");
                    break;
                case 'A':
                    Console.WriteLine("Average");
                    break;
                case 'F':
                    Console.WriteLine("Fail");
                    break;
                default:
                    Console.WriteLine("Invalid grade.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }


        // 11. Valid Date Check
        Console.Write("Enter day: ");
        if (!int.TryParse(Console.ReadLine(), out int day))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter month: ");
        if (!int.TryParse(Console.ReadLine(), out int month))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter year: ");
        if (!int.TryParse(Console.ReadLine(), out int yearr))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        bool isValid = true;
        if (yearr < 1 || month < 1 || month > 12 || day < 1)
        {
            isValid = false;
        }
        else
        {
            int[] daysInMonth = { 31, (DateTime.IsLeapYear(yearr) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (day > daysInMonth[month - 1])
            {
                isValid = false;
            }
        }
        if (isValid)
            Console.WriteLine("The date is valid.");
        else
            Console.WriteLine("The date is invalid.");


        // 12. ATM Withdrawal Simulation
        Console.Write("Insert card? (yes/no): ");
        string? cardInput = Console.ReadLine();
        if (cardInput != null && cardInput.Trim().ToLower() == "yes")
        {
            Console.Write("Enter PIN: ");
            string? pinInput = Console.ReadLine();
            const string correctPin = "1234";
            if (pinInput == correctPin)
            {
            decimal balance = 1000m;
            Console.Write("Enter withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                if (amount <= balance)
                {
                balance -= amount;
                Console.WriteLine($"Withdrawal successful. Remaining balance: {balance:C}");
                }
                else
                {
                Console.WriteLine("Insufficient balance.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            }
            else
            {
            Console.WriteLine("Invalid PIN.");
            }
        }
        else
        {
            Console.WriteLine("Card not inserted.");
        }

        // 13. Profit/Loss Percentage Calculation
        Console.Write("Enter Cost Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal costPrice) || costPrice <= 0)
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter Selling Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal sellingPrice) || sellingPrice <= 0)
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        if (sellingPrice > costPrice)
        {
            decimal profit = sellingPrice - costPrice;
            decimal profitPercent = (profit / costPrice) * 100;
            Console.WriteLine($"Profit: {profit:C}, Profit Percentage: {profitPercent:F2}%");
        }
        else if (costPrice > sellingPrice)
        {
            decimal loss = costPrice - sellingPrice;
            decimal lossPercent = (loss / costPrice) * 100;
            Console.WriteLine($"Loss: {loss:C}, Loss Percentage: {lossPercent:F2}%");
        }
        else
        {
            Console.WriteLine("No profit, no loss.");
        }
        

        // 14. Rock Paper Scissors Game
        Console.WriteLine("Rock Paper Scissors Game!");
        Console.Write("Player 1, enter your choice (rock/paper/scissors): ");
        string? p1 = Console.ReadLine()?.Trim().ToLower();
        Console.Write("Player 2, enter your choice (rock/paper/scissors): ");
        string? p2 = Console.ReadLine()?.Trim().ToLower();

        if ((p1 == "rock" || p1 == "paper" || p1 == "scissors") &&
            (p2 == "rock" || p2 == "paper" || p2 == "scissors"))
        {
            if (p1 == p2)
            {
                Console.WriteLine("It's a tie!");
            }
            else if (p1 == "rock")
            {
                if (p2 == "scissors")
                    Console.WriteLine("Player 1 wins!");
                else
                    Console.WriteLine("Player 2 wins!");
            }
            else if (p1 == "paper")
            {
                if (p2 == "rock")
                    Console.WriteLine("Player 1 wins!");
                else
                    Console.WriteLine("Player 2 wins!");
            }
            else // p1 == "scissors"
            {
                if (p2 == "paper")
                    Console.WriteLine("Player 1 wins!");
                else
                    Console.WriteLine("Player 2 wins!");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Choices must be rock, paper, or scissors.");
        }


        // 15. Simple Calculator
        Console.Write("Enter first number: ");
        if (!double.TryParse(Console.ReadLine(), out double num1))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        Console.Write("Enter operator (+, -, *, /): ");
        string? op = Console.ReadLine();
        Console.Write("Enter second number: ");
        if (!double.TryParse(Console.ReadLine(), out double num2))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        switch (op)
        {
            case "+":
                Console.WriteLine($"Result: {num1 + num2}");
                break;
            case "-":
                Console.WriteLine($"Result: {num1 - num2}");
                break;
            case "*":
                Console.WriteLine($"Result: {num1 * num2}");
                break;
            case "/":
                if (num2 == 0)
                    Console.WriteLine("Cannot divide by zero.");
                else
                    Console.WriteLine($"Result: {num1 / num2}");
                break;
            default:
                Console.WriteLine("Invalid operator.");
                break;
        }

        // 16. Fibonacci Series
        Console.Write("Enter N for Fibonacci series: ");
        if (int.TryParse(Console.ReadLine(), out int fibN) && fibN > 0)
        {
            int fibA = 0, fibB = 1;
            Console.Write("Fibonacci Series: ");
            for (int i = 1; i <= fibN; i++)
            {
            Console.Write(fibA + " ");
            int temp = fibA + fibB;
            fibA = fibB;
            fibB = temp;
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 17. Prime Number Check
        Console.Write("Enter a number to check prime: ");
        if (int.TryParse(Console.ReadLine(), out int primeNum) && primeNum > 1)
        {
            bool isPrime = true;
            for (int i = 2; i <= Math.Sqrt(primeNum); i++)
            {
            if (primeNum % i == 0)
            {
                isPrime = false;
                break;
            }
            }
            Console.WriteLine(isPrime ? $"{primeNum} is prime." : $"{primeNum} is not prime.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 18. Armstrong Number Check
        Console.Write("Enter a number to check Armstrong: ");
        if (int.TryParse(Console.ReadLine(), out int armNum) && armNum >= 0)
        {
            int sum = 0, temp = armNum, digits = armNum.ToString().Length;
            while (temp > 0)
            {
            int d = temp % 10;
            sum += (int)Math.Pow(d, digits);
            temp /= 10;
            }
            Console.WriteLine(sum == armNum ? $"{armNum} is an Armstrong number." : $"{armNum} is not an Armstrong number.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 19. Reverse & Palindrome
        Console.Write("Enter an integer to reverse and check palindrome: ");
        if (int.TryParse(Console.ReadLine(), out int revNum))
        {
            int original = revNum, reversed = 0;
            while (revNum != 0)
            {
            reversed = reversed * 10 + revNum % 10;
            revNum /= 10;
            }
            Console.WriteLine($"Reversed: {reversed}");
            Console.WriteLine(original == reversed ? "Palindrome." : "Not a palindrome.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 20. GCD and LCM
        Console.Write("Enter first number for GCD/LCM: ");
        if (int.TryParse(Console.ReadLine(), out int gcdA))
        {
            Console.Write("Enter second number for GCD/LCM: ");
            if (int.TryParse(Console.ReadLine(), out int gcdB))
            {
            int gcdTempA = gcdA, gcdTempB = gcdB;
            while (gcdTempB != 0)
            {
                int t = gcdTempB;
                gcdTempB = gcdTempA % gcdTempB;
                gcdTempA = t;
            }
            int gcd = Math.Abs(gcdTempA);
            int lcm = Math.Abs(gcdA * gcdB) / (gcd == 0 ? 1 : gcd);
            Console.WriteLine($"GCD: {gcd}, LCM: {lcm}");
            }
            else
            {
            Console.WriteLine("Invalid input.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 21. Pascal's Triangle
        Console.Write("Enter N rows for Pascal's Triangle: ");
        if (int.TryParse(Console.ReadLine(), out int pascalN) && pascalN > 0)
        {
            for (int i = 0; i < pascalN; i++)
            {
            for (int k = 0; k < pascalN - i - 1; k++)
                Console.Write(" ");
            int val = 1;
            for (int j = 0; j <= i; j++)
            {
                Console.Write(val + " ");
                val = val * (i - j) / (j + 1);
            }
            Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 22. Binary to Decimal
        Console.Write("Enter a binary number: ");
        string? binStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(binStr) && binStr.All(c => c == '0' || c == '1'))
        {
            int dec = 0;
            for (int i = 0; i < binStr.Length; i++)
            {
            dec = dec * 2 + (binStr[i] - '0');
            }
            Console.WriteLine($"Decimal: {dec}");
        }
        else
        {
            Console.WriteLine("Invalid binary number.");
        }

        // 23. Diamond Pattern
        Console.Write("Enter diamond height (odd number): ");
        if (int.TryParse(Console.ReadLine(), out int diamondN) && diamondN > 0 && diamondN % 2 == 1)
        {
            int mid = diamondN / 2;
            for (int i = 0; i < diamondN; i++)
            {
            int stars = i <= mid ? 2 * i + 1 : 2 * (diamondN - i - 1) + 1;
            int spaces = (diamondN - stars) / 2;
            Console.Write(new string(' ', spaces));
            Console.WriteLine(new string('*', stars));
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 24. Factorial (Large numbers)
        Console.Write("Enter N for factorial: ");
        if (int.TryParse(Console.ReadLine(), out int factN) && factN >= 0)
        {
            try
            {
            System.Numerics.BigInteger fact = 1;
            for (int i = 2; i <= factN; i++)
                fact *= i;
            Console.WriteLine($"{factN}! = {fact}");
            }
            catch
            {
            Console.WriteLine("Overflow occurred.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 25. Guessing Game
        Random rnd = new Random();
        int secret = rnd.Next(1, 101), guess;
        Console.WriteLine("Guess the secret number (1-100):");
        do
        {
            Console.Write("Your guess: ");
        } while (!int.TryParse(Console.ReadLine(), out guess) || guess != secret);
        Console.WriteLine("Correct! You guessed the secret number.");

        // 26. Sum of Digits (Digital Root)
        Console.Write("Enter a number for digital root: ");
        if (int.TryParse(Console.ReadLine(), out int drNum) && drNum >= 0)
        {
            int result = drNum;
            while (result >= 10)
            {
            int sum = 0;
            while (result > 0)
            {
                sum += result % 10;
                result /= 10;
            }
            result = sum;
            }
            Console.WriteLine($"Digital Root: {result}");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 27. Continue Usage
        Console.WriteLine("Numbers from 1 to 50 (skipping multiples of 3):");
        for (int i = 1; i <= 50; i++)
        {
            if (i % 3 == 0) continue;
            Console.Write(i + " ");
        }
        Console.WriteLine();

        // 28. Menu System
        int menuChoice;
        do
        {
            Console.WriteLine("\nMenu:\n1. Greet\n2. Show Date\n3. Exit");
            Console.Write("Enter choice: ");
            if (!int.TryParse(Console.ReadLine(), out menuChoice))
            {
            Console.WriteLine("Invalid input.");
            continue;
            }
            switch (menuChoice)
            {
            case 1:
                Console.WriteLine("Hello, User!");
                break;
            case 2:
                Console.WriteLine($"Current Date: {DateTime.Now:d}");
                break;
            case 3:
                Console.WriteLine("Exiting menu.");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
            }
        } while (menuChoice != 3);

        // 29. Strong Number
        Console.Write("Enter a number to check Strong Number: ");
        if (int.TryParse(Console.ReadLine(), out int strongNum) && strongNum >= 0)
        {
            int sum = 0, temp = strongNum;
            while (temp > 0)
            {
            int d = temp % 10;
            int fact = 1;
            for (int i = 2; i <= d; i++)
                fact *= i;
            sum += fact;
            temp /= 10;
            }
            Console.WriteLine(sum == strongNum ? $"{strongNum} is a Strong Number." : $"{strongNum} is not a Strong Number.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        // 30. Search with Goto
        int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        Console.Write("Enter number to search in 3x3 array: ");
        if (int.TryParse(Console.ReadLine(), out int searchNum))
        {
            bool found = false;
            for (int i = 0; i < 3; i++)
            {
            for (int j = 0; j < 3; j++)
            {
                if (arr[i, j] == searchNum)
                {
                found = true;
                goto FoundLabel;
                }
            }
            }
        FoundLabel:
            Console.WriteLine(found ? "Found!" : "Not found.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }



    }

}