using System;

// Custom exception for robot safety violations
public class RobotSafetyException : Exception
{
    // Constructor prints the error message immediately
    public RobotSafetyException(string message) : base(message)
    {
        Console.WriteLine(message);
    }
}

// Class responsible for auditing robot hazard risks
public class RobotHazardAuditor
{
    // Calculates the hazard risk based on arm precision, worker density, and machinery state
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

public class CleanseAndInvert
{
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
            if (((int)c) % 2 != 0)
                filtered.Add(c);
        }

        if (filtered.Count == 0)
            return string.Empty;

        filtered.Reverse();

        // Convert even-indexed chars to uppercase
        for (int i = 0; i < filtered.Count; i++)
        {
            if (i % 2 == 0)
                filtered[i] = char.ToUpper(filtered[i]);
        }

        return new string(filtered.ToArray());
    }
}

public class CreatorStats
{
    public string CreatorName { get; set; }
    public double[] WeeklyLikes { get; set; }
    public CreatorStats(string creatorName, double[] weeklyLikes)
    {
        CreatorName = creatorName;
        WeeklyLikes = weeklyLikes;
    }
    public static List<CreatorStats> EngagementBoard { get; } = new List<CreatorStats>();
}

public class Programs
{
    public void RegisterCreator(CreatorStats record)
    {
        CreatorStats.EngagementBoard.Add(record);
    }

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



public class Day7Scenario
{
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
