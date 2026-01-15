using System;

namespace Day20techstack
{
    public class ConstructionEstimateException : Exception
    {
        public ConstructionEstimateException(string message) : base(message) { }
    }

    public class EstimateDetails
    {
        public float ConstructionArea { get; set; }
        public float SiteArea { get; set; }
    }

    public class Estimation
    {
        public static EstimateDetails ValidateConstructionEstimate(float constructionArea, float siteArea)
        {
            if (constructionArea > siteArea)
            {
                throw new ConstructionEstimateException("Sorry your Construction Estimate is not approved.");
            }

            return new EstimateDetails
            {
                ConstructionArea = constructionArea,
                SiteArea = siteArea
            };
        }

        public static void Run()
        {
            try
            {
                Console.WriteLine("Enter Construction Area (in sq ft): ");
                float constructionArea = float.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine("Enter Site Area (in sq ft): ");
                float siteArea = float.Parse(Console.ReadLine() ?? "0");

                EstimateDetails estimate = Estimation.ValidateConstructionEstimate(constructionArea, siteArea);
                Console.WriteLine("Construction Estimate approved successfully!");
            }
            catch (ConstructionEstimateException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numeric values for areas.");
            }
        }
    }
}