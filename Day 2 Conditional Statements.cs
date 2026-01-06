using System; // Import System namespace
public class Day2ConditionalStatements // Main class for Day 2
{
    public static void Run(string[] args) // Main method to run conditional statements demo
    {

        // 1. Height classification 
        Console.Write("Enter your height in cm: "); // Prompt for height
        if (int.TryParse(Console.ReadLine(), out int height)) // Try to parse input as integer
        {
            if (height < 150) // If height is less than 150
                Console.WriteLine("Dwarf"); // Print Dwarf
            else if (height <= 165) // If height is between 150 and 165
                Console.WriteLine("Average"); // Print Average
            else if (height <= 190) // If height is between 166 and 190
                Console.WriteLine("Tall"); // Print Tall
            else // If height is above 190
                Console.WriteLine("Abnormal"); // Print Abnormal
        }
        else // If input is not a valid integer
        {
            Console.WriteLine("Invalid input."); // Print invalid input message
        }


        // 2. Largest of Three Numbers
        Console.Write("Enter first number: "); // Prompt for first number
        if (!int.TryParse(Console.ReadLine(), out int a)) // Try to parse first number
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter second number: "); // Prompt for second number
        if (!int.TryParse(Console.ReadLine(), out int b)) // Try to parse second number
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter third number: "); // Prompt for third number
        if (!int.TryParse(Console.ReadLine(), out int c)) // Try to parse third number
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        int max = a; // Initialize max with first number
        if (b > max) max = b; // Update max if second number is greater
        if (c > max) max = c; // Update max if third number is greater
        Console.WriteLine("The largest number is: " + max); // Print largest number


        // 3. Leap Year Check
        Console.Write("Enter a year: "); // Prompt for year
        if (int.TryParse(Console.ReadLine(), out int year)) // Try to parse year
        {
            if ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0)) // Check leap year condition
                Console.WriteLine(year + " is a leap year."); // Print leap year
            else // If not a leap year
                Console.WriteLine(year + " is not a leap year."); // Print not a leap year
        }
        else // If input is not valid
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }


        // 4. Admission Eligibility
        Console.Write("Enter marks in Mathematics: "); // Prompt for math marks
        if (!int.TryParse(Console.ReadLine(), out int math)) // Try to parse math marks
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter marks in Physics: "); // Prompt for physics marks
        if (!int.TryParse(Console.ReadLine(), out int phys)) // Try to parse physics marks
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter marks in Chemistry: "); // Prompt for chemistry marks
        if (!int.TryParse(Console.ReadLine(), out int chem)) // Try to parse chemistry marks
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        int total = math + phys + chem; // Calculate total marks
        if (math >= 65 && phys >= 55 && chem >= 50 && (total >= 180 || (math + phys) >= 140)) // Check eligibility
            Console.WriteLine("The candidate is eligible for admission."); // Print eligible
        else // If not eligible
            Console.WriteLine("The candidate is not eligible for admission."); // Print not eligible


        // 5. Even or Odd Check
        int number; // Declare number variable
        while (true) // Loop until valid input
        {
            Console.Write("Enter a non-negative integer: "); // Prompt for number
            if (int.TryParse(Console.ReadLine(), out number) && number >= 0) // Try to parse and check non-negative
                break; // Exit loop if valid
            Console.WriteLine("Invalid input. Please enter a non-negative integer."); // Print invalid input
        }
        if (number % 2 == 0) // Check if even
            Console.WriteLine(number + " is even."); // Print even
        else // If odd
            Console.WriteLine(number + " is odd."); // Print odd


        // 6. Vowel or Consonant Check 
        Console.WriteLine("Enter a single alphabet character: "); // Prompt for character
        string? input = Console.ReadLine(); // Read input
        if (!string.IsNullOrEmpty(input) && char.IsLetter(input[0])) // Check if input is a letter
        {
            char ch = char.ToLower(input[0]); // Convert to lowercase
            if ("aeiou".Contains(ch)) // Check if vowel
            {
                Console.WriteLine(ch + " is a vowel."); // Print vowel
            }
            else // If consonant
            {
                Console.WriteLine(ch + " is a consonant."); // Print consonant
            }
        }
        else // If invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 7. Division with Exception Handling
        int da, db; // Declare variables
        Console.WriteLine("Enter a number: "); // Prompt for first number
        while (!int.TryParse(Console.ReadLine(), out da)) // Loop until valid input
        {
            Console.WriteLine("Enter valid input: "); // Print invalid input
        }
        Console.WriteLine("Enter another number: "); // Prompt for second number
        while (!int.TryParse(Console.ReadLine(), out db)) // Loop until valid input
        {
            Console.WriteLine("Enter valid input: "); // Print invalid input
        }
        try // Try division
        {
            int result = da / db; // Perform division
            Console.WriteLine("Result of division: " + result); // Print result
        }
        catch (DivideByZeroException) // Catch divide by zero
        {
            Console.WriteLine("Cannot divide by zero."); // Print error
        }


        // 8. Triangle Type Check
        Console.Write("Enter side 1 of the triangle: "); // Prompt for side 1
        if (!int.TryParse(Console.ReadLine(), out int side1)) // Try to parse side 1
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter side 2 of the triangle: "); // Prompt for side 2
        if (!int.TryParse(Console.ReadLine(), out int side2)) // Try to parse side 2
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter side 3 of the triangle: "); // Prompt for side 3
        if (!int.TryParse(Console.ReadLine(), out int side3)) // Try to parse side 3
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        // Check for triangle validity
        if (side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1) // Check triangle inequality
        {
            if (side1 == side2 && side2 == side3) // All sides equal
            Console.WriteLine("Equilateral triangle."); // Print equilateral
            else if (side1 == side2 || side2 == side3 || side1 == side3) // Two sides equal
            Console.WriteLine("Isosceles triangle."); // Print isosceles
            else // All sides different
            Console.WriteLine("Scalene triangle."); // Print scalene
        }
        else // Not a valid triangle
        {
            Console.WriteLine("The given sides do not form a valid triangle."); // Print invalid triangle
        }

        // 9. Point Quadrant Check
        Console.Write("Enter x coordinate: "); // Prompt for x
        if (!int.TryParse(Console.ReadLine(), out int x)) // Try to parse x
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter y coordinate: "); // Prompt for y
        if (!int.TryParse(Console.ReadLine(), out int y)) // Try to parse y
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }

        if (x == 0 && y == 0) // Check if at origin
            Console.WriteLine("Point is at the origin."); // Print origin
        else if (x == 0) // On Y axis
            Console.WriteLine("Point lies on the Y axis."); // Print Y axis
        else if (y == 0) // On X axis
            Console.WriteLine("Point lies on the X axis."); // Print X axis
        else if (x > 0 && y > 0) // Quadrant I
            Console.WriteLine("Point is in Quadrant I."); // Print Quadrant I
        else if (x < 0 && y > 0) // Quadrant II
            Console.WriteLine("Point is in Quadrant II."); // Print Quadrant II
        else if (x < 0 && y < 0) // Quadrant III
            Console.WriteLine("Point is in Quadrant III."); // Print Quadrant III
        else // Quadrant IV
            Console.WriteLine("Point is in Quadrant IV."); // Print Quadrant IV
    

        // 10. Grade Description using switch 
        Console.Write("Enter grade (E, V, G, A, F): "); // Prompt for grade
        string? gradeInput = Console.ReadLine(); // Read input
        if (!string.IsNullOrEmpty(gradeInput) && gradeInput.Length == 1) // Check input length
        {
            char grade = char.ToUpper(gradeInput[0]); // Convert to uppercase
            switch (grade) // Switch on grade
            {
                case 'E': // Excellent
                    Console.WriteLine("Excellent"); // Print Excellent
                    break;
                case 'V': // Very Good
                    Console.WriteLine("Very Good"); // Print Very Good
                    break;
                case 'G': // Good
                    Console.WriteLine("Good"); // Print Good
                    break;
                case 'A': // Average
                    Console.WriteLine("Average"); // Print Average
                    break;
                case 'F': // Fail
                    Console.WriteLine("Fail"); // Print Fail
                    break;
                default: // Invalid
                    Console.WriteLine("Invalid grade."); // Print invalid
                    break;
            }
        }
        else // If invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }


        // 11. Valid Date Check
        Console.Write("Enter day: "); // Prompt for day
        if (!int.TryParse(Console.ReadLine(), out int day)) // Try to parse day
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter month: "); // Prompt for month
        if (!int.TryParse(Console.ReadLine(), out int month)) // Try to parse month
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter year: "); // Prompt for year
        if (!int.TryParse(Console.ReadLine(), out int yearr)) // Try to parse year
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        bool isValid = true; // Assume valid
        if (yearr < 1 || month < 1 || month > 12 || day < 1) // Check basic validity
        {
            isValid = false; // Set invalid
        }
        else // Check days in month
        {
            int[] daysInMonth = { 31, (DateTime.IsLeapYear(yearr) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }; // Days in each month
            if (day > daysInMonth[month - 1]) // Check if day exceeds max
            {
                isValid = false; // Set invalid
            }
        }
        if (isValid) // If valid
            Console.WriteLine("The date is valid."); // Print valid
        else // If invalid
            Console.WriteLine("The date is invalid."); // Print invalid


        // 12. ATM Withdrawal Simulation
        Console.Write("Insert card? (yes/no): "); // Prompt for card
        string? cardInput = Console.ReadLine(); // Read input
        if (cardInput != null && cardInput.Trim().ToLower() == "yes") // Check if card inserted
        {
            Console.Write("Enter PIN: "); // Prompt for PIN
            string? pinInput = Console.ReadLine(); // Read PIN
            const string correctPin = "1234"; // Set correct PIN
            if (pinInput == correctPin) // Check PIN
            {
            decimal balance = 1000m; // Set initial balance
            Console.Write("Enter withdrawal amount: "); // Prompt for amount
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0) // Parse amount
            {
                if (amount <= balance) // Check if enough balance
                {
                balance -= amount; // Deduct amount
                Console.WriteLine($"Withdrawal successful. Remaining balance: {balance:C}"); // Print success
                }
                else // Not enough balance
                {
                Console.WriteLine("Insufficient balance."); // Print insufficient
                }
            }
            else // Invalid amount
            {
                Console.WriteLine("Invalid amount."); // Print invalid
            }
            }
            else // Invalid PIN
            {
            Console.WriteLine("Invalid PIN."); // Print invalid PIN
            }
        }
        else // Card not inserted
        {
            Console.WriteLine("Card not inserted."); // Print not inserted
        }

        // 13. Profit/Loss Percentage Calculation
        Console.Write("Enter Cost Price: "); // Prompt for cost price
        if (!decimal.TryParse(Console.ReadLine(), out decimal costPrice) || costPrice <= 0) // Parse cost price
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter Selling Price: "); // Prompt for selling price
        if (!decimal.TryParse(Console.ReadLine(), out decimal sellingPrice) || sellingPrice <= 0) // Parse selling price
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }

        if (sellingPrice > costPrice) // Profit case
        {
            decimal profit = sellingPrice - costPrice; // Calculate profit
            decimal profitPercent = (profit / costPrice) * 100; // Calculate profit percent
            Console.WriteLine($"Profit: {profit:C}, Profit Percentage: {profitPercent:F2}%"); // Print profit
        }
        else if (costPrice > sellingPrice) // Loss case
        {
            decimal loss = costPrice - sellingPrice; // Calculate loss
            decimal lossPercent = (loss / costPrice) * 100; // Calculate loss percent
            Console.WriteLine($"Loss: {loss:C}, Loss Percentage: {lossPercent:F2}%"); // Print loss
        }
        else // No profit, no loss
        {
            Console.WriteLine("No profit, no loss."); // Print no profit/loss
        }
        

        // 14. Rock Paper Scissors Game
        Console.WriteLine("Rock Paper Scissors Game!"); // Print game title
        Console.Write("Player 1, enter your choice (rock/paper/scissors): "); // Prompt player 1
        string? p1 = Console.ReadLine()?.Trim().ToLower(); // Read player 1 choice
        Console.Write("Player 2, enter your choice (rock/paper/scissors): "); // Prompt player 2
        string? p2 = Console.ReadLine()?.Trim().ToLower(); // Read player 2 choice

        if ((p1 == "rock" || p1 == "paper" || p1 == "scissors") && // Validate player 1
            (p2 == "rock" || p2 == "paper" || p2 == "scissors")) // Validate player 2
        {
            if (p1 == p2) // Tie case
            {
                Console.WriteLine("It's a tie!"); // Print tie
            }
            else if (p1 == "rock") // Player 1 rock
            {
                if (p2 == "scissors") // Player 2 scissors
                    Console.WriteLine("Player 1 wins!"); // Player 1 wins
                else // Player 2 paper
                    Console.WriteLine("Player 2 wins!"); // Player 2 wins
            }
            else if (p1 == "paper") // Player 1 paper
            {
                if (p2 == "rock") // Player 2 rock
                    Console.WriteLine("Player 1 wins!"); // Player 1 wins
                else // Player 2 scissors
                    Console.WriteLine("Player 2 wins!"); // Player 2 wins
            }
            else // p1 == "scissors"
            {
                if (p2 == "paper") // Player 2 paper
                    Console.WriteLine("Player 1 wins!"); // Player 1 wins
                else // Player 2 rock
                    Console.WriteLine("Player 2 wins!"); // Player 2 wins
            }
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input. Choices must be rock, paper, or scissors."); // Print invalid input
        }


        // 15. Simple Calculator
        Console.Write("Enter first number: "); // Prompt for first number
        if (!double.TryParse(Console.ReadLine(), out double num1)) // Parse first number
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }
        Console.Write("Enter operator (+, -, *, /): "); // Prompt for operator
        string? op = Console.ReadLine(); // Read operator
        Console.Write("Enter second number: "); // Prompt for second number
        if (!double.TryParse(Console.ReadLine(), out double num2)) // Parse second number
        {
            Console.WriteLine("Invalid input."); // Print invalid input
            return; // Exit method
        }

        switch (op) // Switch on operator
        {
            case "+": // Addition
                Console.WriteLine($"Result: {num1 + num2}"); // Print result
                break;
            case "-": // Subtraction
                Console.WriteLine($"Result: {num1 - num2}"); // Print result
                break;
            case "*": // Multiplication
                Console.WriteLine($"Result: {num1 * num2}"); // Print result
                break;
            case "/": // Division
                if (num2 == 0) // Check divide by zero
                    Console.WriteLine("Cannot divide by zero."); // Print error
                else // Valid division
                    Console.WriteLine($"Result: {num1 / num2}"); // Print result
                break;
            default: // Invalid operator
                Console.WriteLine("Invalid operator."); // Print invalid
                break;
        }

        // 16. Fibonacci Series
        Console.Write("Enter N for Fibonacci series: "); // Prompt for N
        if (int.TryParse(Console.ReadLine(), out int fibN) && fibN > 0) // Parse N
        {
            int fibA = 0, fibB = 1; // Initialize first two numbers
            Console.Write("Fibonacci Series: "); // Print label
            for (int i = 1; i <= fibN; i++) // Loop for N terms
            {
            Console.Write(fibA + " "); // Print current term
            int temp = fibA + fibB; // Calculate next term
            fibA = fibB; // Update fibA
            fibB = temp; // Update fibB
            }
            Console.WriteLine(); // New line
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 17. Prime Number Check
        Console.Write("Enter a number to check prime: "); // Prompt for number
        if (int.TryParse(Console.ReadLine(), out int primeNum) && primeNum > 1) // Parse number
        {
            bool isPrime = true; // Assume prime
            for (int i = 2; i <= Math.Sqrt(primeNum); i++) // Loop from 2 to sqrt(num)
            {
            if (primeNum % i == 0) // If divisible
            {
                isPrime = false; // Not prime
                break; // Exit loop
            }
            }
            Console.WriteLine(isPrime ? $"{primeNum} is prime." : $"{primeNum} is not prime."); // Print result
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 18. Armstrong Number Check
        Console.Write("Enter a number to check Armstrong: "); // Prompt for number
        if (int.TryParse(Console.ReadLine(), out int armNum) && armNum >= 0) // Parse number
        {
            int sum = 0, temp = armNum, digits = armNum.ToString().Length; // Initialize variables
            while (temp > 0) // Loop through digits
            {
            int d = temp % 10; // Get digit
            sum += (int)Math.Pow(d, digits); // Add powered digit
            temp /= 10; // Remove digit
            }
            Console.WriteLine(sum == armNum ? $"{armNum} is an Armstrong number." : $"{armNum} is not an Armstrong number."); // Print result
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 19. Reverse & Palindrome
        Console.Write("Enter an integer to reverse and check palindrome: "); // Prompt for number
        if (int.TryParse(Console.ReadLine(), out int revNum)) // Parse number
        {
            int original = revNum, reversed = 0; // Store original and reversed
            while (revNum != 0) // Loop to reverse
            {
            reversed = reversed * 10 + revNum % 10; // Build reversed
            revNum /= 10; // Remove digit
            }
            Console.WriteLine($"Reversed: {reversed}"); // Print reversed
            Console.WriteLine(original == reversed ? "Palindrome." : "Not a palindrome."); // Print palindrome check
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 20. GCD and LCM
        Console.Write("Enter first number for GCD/LCM: "); // Prompt for first number
        if (int.TryParse(Console.ReadLine(), out int gcdA)) // Parse first number
        {
            Console.Write("Enter second number for GCD/LCM: "); // Prompt for second number
            if (int.TryParse(Console.ReadLine(), out int gcdB)) // Parse second number
            {
            int gcdTempA = gcdA, gcdTempB = gcdB; // Temp variables
            while (gcdTempB != 0) // Loop for GCD
            {
                int t = gcdTempB; // Store temp
                gcdTempB = gcdTempA % gcdTempB; // Update gcdTempB
                gcdTempA = t; // Update gcdTempA
            }
            int gcd = Math.Abs(gcdTempA); // Calculate GCD
            int lcm = Math.Abs(gcdA * gcdB) / (gcd == 0 ? 1 : gcd); // Calculate LCM
            Console.WriteLine($"GCD: {gcd}, LCM: {lcm}"); // Print GCD and LCM
            }
            else // Invalid input
            {
            Console.WriteLine("Invalid input."); // Print invalid input
            }
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 21. Pascal's Triangle
        Console.Write("Enter N rows for Pascal's Triangle: "); // Prompt for N
        if (int.TryParse(Console.ReadLine(), out int pascalN) && pascalN > 0) // Parse N
        {
            for (int i = 0; i < pascalN; i++) // Loop for rows
            {
            for (int k = 0; k < pascalN - i - 1; k++) // Print spaces
                Console.Write(" "); // Print space
            int val = 1; // Initialize value
            for (int j = 0; j <= i; j++) // Loop for values
            {
                Console.Write(val + " "); // Print value
                val = val * (i - j) / (j + 1); // Update value
            }
            Console.WriteLine(); // New line
            }
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 22. Binary to Decimal
        Console.Write("Enter a binary number: "); // Prompt for binary
        string? binStr = Console.ReadLine(); // Read input
        if (!string.IsNullOrEmpty(binStr) && binStr.All(c => c == '0' || c == '1')) // Validate binary
        {
            int dec = 0; // Initialize decimal
            for (int i = 0; i < binStr.Length; i++) // Loop through digits
            {
            dec = dec * 2 + (binStr[i] - '0'); // Convert to decimal
            }
            Console.WriteLine($"Decimal: {dec}"); // Print decimal
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid binary number."); // Print invalid
        }

        // 23. Diamond Pattern
        Console.Write("Enter diamond height (odd number): "); // Prompt for height
        if (int.TryParse(Console.ReadLine(), out int diamondN) && diamondN > 0 && diamondN % 2 == 1) // Parse and check odd
        {
            int mid = diamondN / 2; // Calculate middle
            for (int i = 0; i < diamondN; i++) // Loop for rows
            {
            int stars = i <= mid ? 2 * i + 1 : 2 * (diamondN - i - 1) + 1; // Calculate stars
            int spaces = (diamondN - stars) / 2; // Calculate spaces
            Console.Write(new string(' ', spaces)); // Print spaces
            Console.WriteLine(new string('*', stars)); // Print stars
            }
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 24. Factorial (Large numbers)
        Console.Write("Enter N for factorial: "); // Prompt for N
        if (int.TryParse(Console.ReadLine(), out int factN) && factN >= 0) // Parse N
        {
            try // Try block for overflow
            {
            System.Numerics.BigInteger fact = 1; // Initialize factorial
            for (int i = 2; i <= factN; i++) // Loop for factorial
                fact *= i; // Multiply
            Console.WriteLine($"{factN}! = {fact}"); // Print factorial
            }
            catch // Catch overflow
            {
            Console.WriteLine("Overflow occurred."); // Print overflow
            }
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 25. Guessing Game
        Random rnd = new Random(); // Create random object
        int secret = rnd.Next(1, 101), guess; // Generate secret number
        Console.WriteLine("Guess the secret number (1-100):"); // Print prompt
        do // Loop until correct guess
        {
            Console.Write("Your guess: "); // Prompt for guess
        } while (!int.TryParse(Console.ReadLine(), out guess) || guess != secret); // Check guess
        Console.WriteLine("Correct! You guessed the secret number."); // Print success

        // 26. Sum of Digits (Digital Root)
        Console.Write("Enter a number for digital root: "); // Prompt for number
        if (int.TryParse(Console.ReadLine(), out int drNum) && drNum >= 0) // Parse number
        {
            int result = drNum; // Initialize result
            while (result >= 10) // Loop until single digit
            {
            int sum = 0; // Initialize sum
            while (result > 0) // Loop through digits
            {
                sum += result % 10; // Add digit
                result /= 10; // Remove digit
            }
            result = sum; // Update result
            }
            Console.WriteLine($"Digital Root: {result}"); // Print digital root
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 27. Continue Usage
        Console.WriteLine("Numbers from 1 to 50 (skipping multiples of 3):"); // Print label
        for (int i = 1; i <= 50; i++) // Loop from 1 to 50
        {
            if (i % 3 == 0) continue; // Skip multiples of 3
            Console.Write(i + " "); // Print number
        }
        Console.WriteLine(); // New line

        // 28. Menu System
        int menuChoice; // Declare menu choice
        do // Loop for menu
        {
            Console.WriteLine("\nMenu:\n1. Greet\n2. Show Date\n3. Exit"); // Print menu
            Console.Write("Enter choice: "); // Prompt for choice
            if (!int.TryParse(Console.ReadLine(), out menuChoice)) // Parse choice
            {
            Console.WriteLine("Invalid input."); // Print invalid input
            continue; // Continue loop
            }
            switch (menuChoice) // Switch on choice
            {
            case 1: // Greet
                Console.WriteLine("Hello, User!"); // Print greeting
                break;
            case 2: // Show date
                Console.WriteLine($"Current Date: {DateTime.Now:d}"); // Print date
                break;
            case 3: // Exit
                Console.WriteLine("Exiting menu."); // Print exit
                break;
            default: // Invalid
                Console.WriteLine("Invalid choice."); // Print invalid
                break;
            }
        } while (menuChoice != 3); // Loop until exit

        // 29. Strong Number
        Console.Write("Enter a number to check Strong Number: "); // Prompt for number
        if (int.TryParse(Console.ReadLine(), out int strongNum) && strongNum >= 0) // Parse number
        {
            int sum = 0, temp = strongNum; // Initialize variables
            while (temp > 0) // Loop through digits
            {
            int d = temp % 10; // Get digit
            int fact = 1; // Initialize factorial
            for (int i = 2; i <= d; i++) // Loop for factorial
                fact *= i; // Multiply
            sum += fact; // Add factorial
            temp /= 10; // Remove digit
            }
            Console.WriteLine(sum == strongNum ? $"{strongNum} is a Strong Number." : $"{strongNum} is not a Strong Number."); // Print result
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }

        // 30. Search with Goto
        int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }; // Initialize 3x3 array
        Console.Write("Enter number to search in 3x3 array: "); // Prompt for number
        if (int.TryParse(Console.ReadLine(), out int searchNum)) // Parse number
        {
            bool found = false; // Initialize found flag
            for (int i = 0; i < 3; i++) // Loop rows
            {
            for (int j = 0; j < 3; j++) // Loop columns
            {
                if (arr[i, j] == searchNum) // Check if found
                {
                found = true; // Set found
                goto FoundLabel; // Jump to label
                }
            }
            }
        FoundLabel:
            Console.WriteLine(found ? "Found!" : "Not found."); // Print result
        }
        else // Invalid input
        {
            Console.WriteLine("Invalid input."); // Print invalid input
        }



    }

}