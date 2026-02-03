using System;

namespace Day32
{
// Custom exception for robot safety violations
    public class RobotSafetyException : Exception
    {
        public RobotSafetyException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }

    // Class responsible for auditing robot hazard risks
    public class RobotHazardAuditor
    {
        public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)
        {
            // Validate arm precision
            if (armPrecision < 0.0 || armPrecision > 1.0)
                throw new RobotSafetyException("Error: Arm precision must be 0.0-1.0");

            // Validate worker density
            if (workerDensity < 1 || workerDensity > 20)
                throw new RobotSafetyException("Error: Worker density must be 1-20");

            double machineRiskFactor;

            if (machineryState == "Worn")
                machineRiskFactor = 1.3;
            else if (machineryState == "Faulty")
                machineRiskFactor = 2.0;
            else if (machineryState == "Critical")
                machineRiskFactor = 3.0;
            else
                throw new RobotSafetyException("Error: Unsupported machinery state");

            double hazardRisk =
                ((1.0 - armPrecision) * 15.0) +
                (workerDensity * machineRiskFactor);

            return hazardRisk;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Arm precision
                Console.WriteLine("Enter Arm Precision (0.0 - 1.0):");
                string? armPrecisionInput = Console.ReadLine();
    
                if (string.IsNullOrWhiteSpace(armPrecisionInput))
                    throw new RobotSafetyException("Error: Arm precision input cannot be empty.");
    
                double armPrecision = double.Parse(armPrecisionInput!);
    
                // Worker density
                Console.WriteLine("Enter Worker Density (1 - 20):");
                string? workerDensityInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(workerDensityInput))
                    throw new RobotSafetyException("Error: Worker density input cannot be empty.");

                int workerDensity = int.Parse(workerDensityInput);

                // Machinery state
                Console.WriteLine("Enter Machinery State (Worn/Faulty/Critical):");
                string? machineryStateInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(machineryStateInput))
                    throw new RobotSafetyException("Error: Machinery state input cannot be empty.");

                string machineryState = machineryStateInput;

                // Calculate risk
                RobotHazardAuditor auditor = new RobotHazardAuditor();

                double risk = auditor.CalculateHazardRisk(
                    armPrecision,
                    workerDensity,
                    machineryState
                );

                Console.WriteLine("Robot Hazard Risk Score: " + risk);
            }
            catch (RobotSafetyException ex)
            {
                Console.WriteLine("Robot Safety Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }
    }
}