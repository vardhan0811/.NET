using System;

namespace Day32
{
    // Custom exception for robot safety violations
    public class RobotSafetyException : Exception
    {
        public RobotSafetyException(string message) : base(message)
        {
            // Print the error message when the exception is created
            Console.WriteLine(message);
        }
    }

    // Class responsible for auditing robot hazard risks
    public class RobotHazardAuditor
    {
        // Calculates the hazard risk based on arm precision, worker density, and machinery state
        public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
        {
            // Validate arm precision input
            if (armPrecision < 0.0 || armPrecision > 1.0)
                throw new RobotSafetyException("Error: Arm precision must be 0.0-1.0");

            // Validate worker density input
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

            // Calculate the hazard risk score
            double hazardRisk = ((1.0 - armPrecision) * 15.0) + (workerDensity * machineRiskFactor);
            return hazardRisk;
        }
    }

    public class FactoryRobot
    {
        public static void Run()
        {
            try
            {
                // Prompt user for arm precision input
                Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
                double armPrecision = double.Parse(Console.ReadLine());

                // Prompt user for worker density input
                Console.WriteLine("Enter Worker Density (1 - 20):");
                int workerDensity = int.Parse(Console.ReadLine());

                // Prompt user for machinery state input
                Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
                string machineryState = Console.ReadLine();

                // Create an auditor and calculate the risk
                RobotHazardAuditor auditor = new RobotHazardAuditor();
                double risk = auditor.CalculateHazardRisk(armPrecision, workerDensity, machineryState);

                // Output the calculated risk score
                Console.WriteLine($"Robot Hazard Risk Score: {risk}");
            }
            catch (RobotSafetyException)
            {
                // Exception message already printed in exception constructor
            }
            catch (Exception)
            {
                // For any unexpected parsing errors, do nothing as per requirements
            }
        }
    }
}