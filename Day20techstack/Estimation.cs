using System;

namespace Day20techstack
{
    /// <summary>
    /// Custom exception thrown when construction area exceeds site area.
    /// </summary>
    public class ConstructionEstimateException : Exception
    {
        public ConstructionEstimateException(string message) : base(message) { }
    }

    /// <summary>
    /// Holds details about a construction estimate.
    /// </summary>
    public class EstimateDetails
    {
        public float ConstructionArea { get; set; }
        public float SiteArea { get; set; }
    }

    /// <summary>
    /// Provides methods to validate and process construction estimates.
    /// </summary>
    public class Estimation
    {
        /// <summary>
        /// Validates that the construction area does not exceed the site area.
        /// Throws ConstructionEstimateException if validation fails.
        /// </summary>
        /// <param name="constructionArea">Area to be constructed</param>
        /// <param name="siteArea">Total site area</param>
        /// <returns>EstimateDetails object if validation passes</returns>
        public static EstimateDetails ValidateConstructionEstimate(float constructionArea, float siteArea)
        {
            // Check if construction area is greater than site area
            if (constructionArea > siteArea)
            {
                throw new ConstructionEstimateException("Sorry your Construction Estimate is not approved.");
            }

            // Return estimate details if validation passes
            return new EstimateDetails
            {
                ConstructionArea = constructionArea,
                SiteArea = siteArea
            };
        }

        /// <summary>
        /// Runs the construction estimate validation process by taking user input.
        /// </summary>
        public static void Run()
        {
            try
            {
                Console.WriteLine("Enter Construction Area (in sq ft): ");
                float constructionArea = float.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine("Enter Site Area (in sq ft): ");
                float siteArea = float.Parse(Console.ReadLine() ?? "0");

                // Validate the construction estimate
                EstimateDetails estimate = Estimation.ValidateConstructionEstimate(constructionArea, siteArea);
                Console.WriteLine("Construction Estimate approved successfully!");
            }
            catch (ConstructionEstimateException ex)
            {
                // Handle custom exception for invalid estimate
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException)
            {
                // Handle invalid input format
                Console.WriteLine("Invalid input. Please enter numeric values for areas.");
            }
        }
    }
}