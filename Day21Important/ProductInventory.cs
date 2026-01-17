using System;

namespace Day21Important
{
    /// <summary>
    /// Interface representing a product with name, category, price, and quantity.
    /// </summary>
    public interface IProduct
    {
        string Name { get; set; }
        string Category { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
    }

    /// <summary>
    /// Interface for inventory operations.
    /// </summary>
    public interface IInventory
    {
        void AddProduct(IProduct product);
        void RemoveProduct(IProduct product);
        int CalculateTotalValue();
        List<IProduct> GetProductsByCategory(string category);
        Dictionary<string, int> GetProductByCategoryWithCount();
        List<IProduct> SearchAllProducts(string name);
        Dictionary<string, List<IProduct>> GetAllProductsByCategory();
    }

    /// <summary>
    /// Represents a product in the inventory.
    /// </summary>
    public class Product : IProduct
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Implements the inventory system for managing products.
    /// </summary>
    public class Inventory : IInventory
    {
        private List<IProduct> _products = new List<IProduct>();

        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        public void AddProduct(IProduct product)
        {
            _products.Add(product);
        }

        /// <summary>
        /// Removes a product from the inventory.
        /// </summary>
        public void RemoveProduct(IProduct product)
        {
            _products.Remove(product);
        }

        /// <summary>
        /// Calculates the total value of all products in the inventory.
        /// </summary>
        public int CalculateTotalValue()
        {
            return (int)_products.Sum(p => p.Price * p.Quantity);
        }

        /// <summary>
        /// Returns a list of products in a specific category.
        /// </summary>
        public List<IProduct> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Returns a dictionary with product counts by category.
        /// </summary>
        public Dictionary<string, int> GetProductByCategoryWithCount()
        {
            return _products.GroupBy(p => p.Category)
                            .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Searches for products by name (case-insensitive).
        /// </summary>
        public List<IProduct> SearchAllProducts(string name)
        {
            return _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Returns all products grouped by category.
        /// </summary>
        public Dictionary<string, List<IProduct>> GetAllProductsByCategory()
        {
            return _products.GroupBy(p => p.Category)
                            .ToDictionary(g => g.Key, g => g.ToList());
        }
    }

    /// <summary>
    /// Entry point for the Product Inventory system.
    /// </summary>
    public class ProductInventory
    {
        /// <summary>
        /// Runs the product inventory demo by adding, removing, and displaying products.
        /// </summary>
        public static void Run()
        {
            Inventory inventory = new Inventory();

            // Adding products
            inventory.AddProduct(new Product { Name = "Laptop", Category = "Electronics", Price = 999.99m, Quantity = 5 });
            inventory.AddProduct(new Product { Name = "Smartphone", Category = "Electronics", Price = 499.99m, Quantity = 10 });
            inventory.AddProduct(new Product { Name = "Desk", Category = "Furniture", Price = 199.99m, Quantity = 2 });

            // Removing a product
            inventory.RemoveProduct(new Product { Name = "Desk", Category = "Furniture", Price = 199.99m, Quantity = 2 });

            // Calculating total inventory value
            int totalValue = inventory.CalculateTotalValue();
            Console.WriteLine($"Total Inventory Value: {totalValue}");

            // Getting products by category
            var electronics = inventory.GetProductsByCategory("Electronics");
            Console.WriteLine($"Electronics Products: {string.Join(", ", electronics.Select(p => p.Name))}");

            // Getting product count by category
            var productCountByCategory = inventory.GetProductByCategoryWithCount();
            foreach (var kvp in productCountByCategory)
            {
                Console.WriteLine($"Category: {kvp.Key}, Count: {kvp.Value}");
            }

            // Searching products
            var searchResults = inventory.SearchAllProducts("Laptop");
            Console.WriteLine($"Search Results: {string.Join(", ", searchResults.Select(p => p.Name))}");

            // Getting all products by category
            var allProductsByCategory = inventory.GetAllProductsByCategory();
            foreach (var kvp in allProductsByCategory)
            {
                Console.WriteLine($"Category: {kvp.Key}, Products: {string.Join(", ", kvp.Value.Select(p => p.Name))}");
            }
        }
    }
}