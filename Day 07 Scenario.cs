using System;

/// <summary>
/// Custom exception for robot safety violations.
/// </summary>
public class RobotSafetyException : Exception
{
    /// <summary>
    /// Constructor prints the error message immediately.
    /// </summary>
    /// <param name="message">Error message.</param>
    public RobotSafetyException(string message) : base(message)
    {
        Console.WriteLine(message);
    }
}

/// <summary>
/// Class responsible for auditing robot hazard risks.
/// </summary>
public class RobotHazardAuditor
{
    /// <summary>
    /// Calculates the hazard risk based on arm precision, worker density, and machinery state.
    /// </summary>
    /// <param name="armPrecision">Precision of the robot arm (0.0-1.0).</param>
    /// <param name="workerDensity">Number of workers in the area (1-20).</param>
    /// <param name="machineryState">State of the machinery (Worn/Faulty/Critical).</param>
    /// <returns>Hazard risk score.</returns>
    public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
    {
        // Validate arm precision is within the allowed range
        if (armPrecision < 0.0 || armPrecision > 1.0)
            throw new RobotSafetyException("Error: Arm precision must be 0.0-1.0");

        // Validate worker density is within the allowed range
        if (workerDensity < 1 || workerDensity > 20)
            throw new RobotSafetyException("Error: Worker density must be 1-20");

        double machineRiskFactor;

        // Determine risk factor based on machinery state
        if (machineryState == "Worn")
            machineRiskFactor = 1.3;
        else if (machineryState == "Faulty")
            machineRiskFactor = 2.0;
        else if (machineryState == "Critical")
            machineRiskFactor = 3.0;
        else
            throw new RobotSafetyException("Error: Unsupported machinery state");

        // Calculate the final hazard risk score
        double hazardRisk = ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor);
        return hazardRisk;
    }
}

/// <summary>
/// Provides string processing functionality for cleansing and inverting input.
/// </summary>
public class CleanseAndInvert
{
    /// <summary>
    /// Processes the input string by filtering, reversing, and formatting.
    /// </summary>
    /// <param name="input">Input string.</param>
    /// <returns>Processed string or empty if invalid.</returns>
    public string ProcessInput(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 6)
            return string.Empty;

        // Check for invalid characters (anything not a-z or A-Z)
        foreach (char c in input)
        {
            if (!char.IsLetter(c))
                return string.Empty;
        }

        string lower = input.ToLower();
        // Remove characters with even ASCII codes
        var filtered = new List<char>();
        foreach (char c in lower)
        {
            // Keep only characters with odd ASCII codes
            if (((int)c) % 2 != 0)
                filtered.Add(c);
        }

        if (filtered.Count == 0)
            return string.Empty;

        // Reverse the filtered characters
        filtered.Reverse();

        // Convert even-indexed chars to uppercase
        for (int i = 0; i < filtered.Count; i++)
        {
            if (i % 2 == 0)
                filtered[i] = char.ToUpper(filtered[i]);
        }

        // Return the processed string
        return new string(filtered.ToArray());
    }
}


/// <summary>
/// Stores creator statistics and engagement board.
/// </summary>
public class CreatorStats
{
    /// <summary>
    /// Gets or sets the creator's name.
    /// </summary>
    public string CreatorName { get; set; }
    /// <summary>
    /// Gets or sets the weekly likes for the creator.
    /// </summary>
    public double[] WeeklyLikes { get; set; }
    /// <summary>
    /// Initializes a new creator stats record.
    /// </summary>
    /// <param name="creatorName">Creator name.</param>
    /// <param name="weeklyLikes">Weekly likes array.</param>
    public CreatorStats(string creatorName, double[] weeklyLikes)
    {
        CreatorName = creatorName;
        WeeklyLikes = weeklyLikes;
    }
    /// <summary>
    /// Static engagement board for all creators.
    /// </summary>
    public static List<CreatorStats> EngagementBoard { get; } = new List<CreatorStats>();
}

/// <summary>
/// Provides program logic for creator registration and statistics.
/// </summary>
public class Programs
{
    /// <summary>
    /// Registers a creator record to the engagement board.
    /// </summary>
    /// <param name="record">Creator record.</param>
    public void RegisterCreator(CreatorStats record)
    {
        CreatorStats.EngagementBoard.Add(record);
    }

    /// <summary>
    /// Gets the count of top posts for each creator above a like threshold.
    /// </summary>
    /// <param name="records">List of creator records.</param>
    /// <param name="likeThreshold">Threshold for likes.</param>
    /// <returns>Dictionary of creator names and top post counts.</returns>
    public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
    {
        var result = new Dictionary<string, int>();
        foreach (var creator in records)
        {
            int count = 0;
            foreach (var likes in creator.WeeklyLikes)
            {
                if (likes >= likeThreshold)
                    count++;
            }
            if (count > 0)
                result[creator.CreatorName] = count;
        }
        return result;
    }

    /// <summary>
    /// Calculates the average weekly likes across all creators.
    /// </summary>
    /// <returns>Average weekly likes.</returns>
    public double CalculateAverageLikes()
    {
        int totalWeeks = 0;
        double totalLikes = 0;
        foreach (var creator in CreatorStats.EngagementBoard)
        {
            totalWeeks += creator.WeeklyLikes.Length;
            foreach (var likes in creator.WeeklyLikes)
                totalLikes += likes;
        }
        if (totalWeeks == 0)
            return 0;
        return Math.Round(totalLikes / totalWeeks);
    }

    /// <summary>
    /// Runs the creator statistics program.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    public static void Run(string[] args)
    {
        Programs p = new Programs();
        while (true)
        {
            Console.WriteLine("1. Register Creator");
            Console.WriteLine("2. Show Top Posts");
            Console.WriteLine("3. Calculate Average Likes");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Enter your choice:");
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.WriteLine("Enter Creator Name:");
                string? name = Console.ReadLine();
                double[] likes = new double[4];
                Console.WriteLine("Enter weekly likes (Week 1 to 4):");
                for (int i = 0; i < 4; i++)
                {
                    likes[i] = double.Parse(Console.ReadLine()!);
                }
                CreatorStats record = new CreatorStats(name ?? string.Empty, likes);
                p.RegisterCreator(record);
                Console.WriteLine("Creator registered successfully");
            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter like threshold:");
                double threshold = double.Parse(Console.ReadLine()!);
                var result = p.GetTopPostCounts(CreatorStats.EngagementBoard, threshold);
                if (result.Count == 0)
                {
                    Console.WriteLine("No top-performing posts this week");
                }
                else
                {
                    foreach (var kv in result)
                    {
                        Console.WriteLine($"{kv.Key} - {kv.Value}");
                    }
                }
            }
            else if (choice == "3")
            {
                double avg = p.CalculateAverageLikes();
                Console.WriteLine($"Overall average weekly likes: {avg}");
            }
            else if (choice == "4")
            {
                Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                break;
            }
        }
    }
}

/// <summary>
/// Main scenario class for Day 7 exercises.
/// </summary>
public class Day07Scenario
{
    /// <summary>
    /// Runs the robot hazard and string processing scenarios.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    public static void Run(string[] args)
    {
        try
        {
            // Prompt user for robot arm precision
            Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
            double armPrecision = double.Parse(Console.ReadLine()!);

            // Prompt user for number of workers in the area
            Console.WriteLine("Enter Worker Density (1 - 20):");
            int workerDensity = int.Parse(Console.ReadLine()!);

            // Prompt user for the current state of the machinery
            Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
            string machineryState = Console.ReadLine()!;

            // Create an auditor and calculate the risk score
            RobotHazardAuditor auditor = new RobotHazardAuditor();
            double risk = auditor.CalculateHazardRisk(armPrecision, workerDensity, machineryState);

            // Output the calculated risk score
            Console.WriteLine($"Robot Hazard Risk Score: {risk}");
        }
        catch (RobotSafetyException)
        {
            // Error message already displayed by the exception itself
        }
        catch (Exception)
        {
            // Silently ignore any other unexpected errors
        }

        // String processing scenario
        Console.WriteLine("Enter the word");
        string? input = Console.ReadLine();

        CleanseAndInvert p = new CleanseAndInvert();
        string result = p.ProcessInput(input ?? string.Empty);

        if (string.IsNullOrEmpty(result))
            Console.WriteLine("Invalid Input");
        else
            Console.WriteLine($"The generated key is - {result}");
    }
}
