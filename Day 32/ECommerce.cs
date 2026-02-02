using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
// Represents a product in the inventory
public class Product
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; } 
    public double Price { get; set; }
    public int StockQuantity { get; set; }

    // Constructor to initialize product details
    public Product(string code, string name, string category, double price, int stock)
    {
        ProductCode = code;
        ProductName = name;
        Category = category;
        Price = price;
        StockQuantity = stock;
    }
}

// Manages the inventory of products
public class InventoryManager
{
    private List<Product> products = new List<Product>();
    private int productCounter = 1;

    // Adds a new product to the inventory
    public void AddProduct(string name, string category, double price, int stock)
    {
        string code = $"P{productCounter:D3}";
        products.Add(new Product(code, name, category, price, stock));
        productCounter++;
    }

    // Groups products by their category and returns a sorted dictionary
    public SortedDictionary<string, List<Product>> GroupProductsByCategory()
    {
        var grouped = new SortedDictionary<string, List<Product>>();
        foreach (var product in products)
        {
            if (!grouped.ContainsKey(product.Category))
                grouped[product.Category] = new List<Product>();
            grouped[product.Category].Add(product);
        }
        return grouped;
    }

    // Updates the stock quantity after a sale
    public bool UpdateStock(string productCode, int quantity)
    {
        var product = products.FirstOrDefault(p => p.ProductCode == productCode);
        if (product == null || product.StockQuantity < quantity)
            return false;
        product.StockQuantity -= quantity;
        return true;
    }

    // Returns a list of products below a specified price
    public List<Product> GetProductsBelowPrice(double maxPrice)
    {
        return products.Where(p => p.Price < maxPrice).ToList();
    }

    // Returns a summary of stock quantities grouped by category
    public Dictionary<string, int> GetCategoryStockSummary()
    {
        return products
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.Sum(p => p.StockQuantity));
    }

    // Returns all products in the inventory
    public List<Product> GetAllProducts()
    {
        return products;
    }
}

// Main class to run the E-Commerce inventory application
class ECommerce
{
    public static void Run()
    {
        InventoryManager manager = new InventoryManager();

        // Sample products added to inventory
        manager.AddProduct("Laptop", "Electronics", 1200.0, 10);
        manager.AddProduct("T-Shirt", "Clothing", 25.0, 50);
        manager.AddProduct("Novel", "Books", 15.0, 30);
        manager.AddProduct("Headphones", "Electronics", 80.0, 20);

        while (true)
        {
            // Display menu options
            Console.WriteLine("\n--- E-Commerce Inventory Menu ---");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Display Products Grouped by Category");
            Console.WriteLine("3. Update Stock After Sale");
            Console.WriteLine("4. Find Products Below Price");
            Console.WriteLine("5. Show Inventory Summary");
            Console.WriteLine("6. List All Products");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // Add a new product
                    Console.Write("Product Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Category: ");
                    string category = Console.ReadLine();
                    Console.Write("Price: ");
                    double price = double.TryParse(Console.ReadLine(), out price) ? price : 0;
                    Console.Write("Stock Quantity: ");
                    int stock = int.TryParse(Console.ReadLine(), out stock) ? stock : 0;
                    manager.AddProduct(name, category, price, stock);
                    Console.WriteLine("Product added.");
                    break;

                case "2":
                    // Display products grouped by category
                    var grouped = manager.GroupProductsByCategory();
                    foreach (var cat in grouped.Keys)
                    {
                        Console.WriteLine($"Category: {cat}");
                        foreach (var product in grouped[cat])
                            Console.WriteLine($"  {product.ProductCode}: {product.ProductName} (${product.Price}) Stock: {product.StockQuantity}");
                    }
                    break;

                case "3":
                    // Update stock after a sale
                    Console.Write("Enter Product Code: ");
                    string code = Console.ReadLine();
                    Console.Write("Quantity to Sell: ");
                    int qty = int.TryParse(Console.ReadLine(), out qty) ? qty : 0;
                    bool updated = manager.UpdateStock(code, qty);
                    Console.WriteLine(updated ? "Stock updated." : "Update failed (invalid code or insufficient stock).");
                    break;

                case "4":
                    // Find products below a specified price
                    Console.Write("Enter maximum price: ");
                    double maxPrice = double.TryParse(Console.ReadLine(), out maxPrice) ? maxPrice : 0;
                    var cheapProducts = manager.GetProductsBelowPrice(maxPrice);
                    Console.WriteLine($"Products below ${maxPrice}:");
                    foreach (var product in cheapProducts)
                        Console.WriteLine($"  {product.ProductName} (${product.Price})");
                    break;

                case "5":
                    // Show inventory summary by category
                    var summary = manager.GetCategoryStockSummary();
                    Console.WriteLine("Inventory Summary:");
                    foreach (var kvp in summary)
                        Console.WriteLine($"  {kvp.Key}: {kvp.Value} items");
                    break;

                case "6":
                    // List all products in inventory
                    var allProducts = manager.GetAllProducts();
                    Console.WriteLine("All Products:");
                    foreach (var product in allProducts)
                        Console.WriteLine($"{product.ProductCode}: {product.ProductName} ({product.Category}) - ${product.Price}, Stock: {product.StockQuantity}");
                    break;

                case "0":
                    // Exit the application
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    // Handle invalid menu option
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
}