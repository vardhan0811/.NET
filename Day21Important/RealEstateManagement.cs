using System;

namespace Day21Important
{
    /// <summary>
    /// Represents a real estate property listing.
    /// </summary>
    public class RealEstateListing
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? Location { get; set; }
    }

    /// <summary>
    /// Manages a collection of real estate listings and provides operations for them.
    /// </summary>
    public class RealEstateApp
    {
        // List to store all real estate listings
        private List<RealEstateListing> _listings = new List<RealEstateListing>();

        /// <summary>
        /// Adds a new listing to the collection.
        /// </summary>
        public void AddListing(RealEstateListing listing)
        {
            _listings.Add(listing);
        }

        /// <summary>
        /// Removes a listing by its ID.
        /// </summary>
        public void RemoveListing(int id)
        {
            _listings.RemoveAll(l => l.ID == id);
        }

        /// <summary>
        /// Updates an existing listing with new details.
        /// </summary>
        public void UpdateListing(RealEstateListing updatedListing)
        {
            var existingListing = _listings.FirstOrDefault(l => l.ID == updatedListing.ID);
            if (existingListing != null)
            {
                existingListing.Title = updatedListing.Title;
                existingListing.Description = updatedListing.Description;
                existingListing.Price = updatedListing.Price;
                existingListing.Location = updatedListing.Location;
            }
        }

        /// <summary>
        /// Returns all listings.
        /// </summary>
        public List<RealEstateListing> GetAllListings()
        {
            return _listings;
        }

        /// <summary>
        /// Returns listings filtered by location (case-insensitive).
        /// </summary>
        public List<RealEstateListing> GetListingsByLocation(string location)
        {
            return _listings.Where(l => l.Location != null && l.Location.Equals(location, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Returns listings within a specified price range.
        /// </summary>
        public List<RealEstateListing> GetListingsByPriceRange(int minPrice, int maxPrice)
        {
            return _listings.Where(l => l.Price >= minPrice && l.Price <= maxPrice).ToList();
        }
    }

    /// <summary>
    /// Entry point for the real estate management demo.
    /// </summary>
    public class RealEstateManagement
    {
        /// <summary>
        /// Runs the real estate management demo by adding, displaying, and filtering listings.
        /// </summary>
        public static void Run()
        {
            RealEstateApp app = new RealEstateApp();

            // Create and add first listing
            RealEstateListing listing1 = new RealEstateListing
            {
                ID = 1,
                Title = "Cozy Apartment",
                Description = "A cozy apartment in the city center.",
                Price = 150000,
                Location = "New York"
            };

            // Create and add second listing
            RealEstateListing listing2 = new RealEstateListing
            {
                ID = 2,
                Title = "Spacious Villa",
                Description = "A spacious villa with a beautiful garden.",
                Price = 500000,
                Location = "Los Angeles"
            };

            app.AddListing(listing1);
            app.AddListing(listing2);

            // Display all listings
            var allListings = app.GetAllListings();
            Console.WriteLine("All Listings:");
            foreach (var listing in allListings)
            {
                Console.WriteLine($"{listing.ID}: {listing.Title} - {listing.Price} - {listing.Location}");
            }

            // Display listings in New York
            var nyListings = app.GetListingsByLocation("New York");
            Console.WriteLine("\nListings in New York:");
            foreach (var listing in nyListings)
            {
                Console.WriteLine($"{listing.ID}: {listing.Title} - {listing.Price} - {listing.Location}");
            }

            // Display listings in Los Angeles
            var laListings = app.GetListingsByLocation("Los Angeles");
            Console.WriteLine("\nListings in Los Angeles:");
            foreach (var listing in laListings)
            {
                Console.WriteLine($"{listing.ID}: {listing.Title} - {listing.Price} - {listing.Location}");
            }

            // Display affordable listings in price range
            var affordableListings = app.GetListingsByPriceRange(100000, 300000);
            Console.WriteLine("\nAffordable Listings (100000 - 300000):");
            foreach (var listing in affordableListings)
            {
                Console.WriteLine($"{listing.ID}: {listing.Title} - {listing.Price} - {listing.Location}");
            }
        }
    }
}