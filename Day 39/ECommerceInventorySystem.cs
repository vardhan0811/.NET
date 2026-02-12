using System;
using System.Collections.Generic;
using System.Linq;

namespace Day39
{
    // =========================
    // Base Product Interface
    // =========================
    public interface IProduct
    {
        int Id { get; }
        string? Name { get; }
        decimal Price { get; set; }
        Category Category { get; }
    }

    public enum Category
    {
        Electronics,
        Clothing,
        Books,
        Groceries
    }

    // =========================
    // Generic Product Repository
    // =========================
    public class ProductRepository<T> where T : class, IProduct
    {
        private List<T> _products = new List<T>();

        // Add product with validation
        public void AddProduct(T product)
        {
            if (product == null)
                throw new ArgumentNullException("Product cannot be null");

            if (_products.Any(p => p.Id == product.Id))
                throw new InvalidOperationException("Product ID must be unique");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty");

            if (product.Price <= 0)
                throw new ArgumentException("Price must be positive");

            _products.Add(product);
        }

        // Find products using predicate
        public IEnumerable<T> FindProducts(Func<T, bool> predicate)
        {
            return _products.Where(predicate);
        }

        // Calculate total inventory value
        public decimal CalculateTotalValue()
        {
            return _products.Sum(p => p.Price);
        }

        // Expose products safely
        public List<T> GetAllProducts()
        {
            return _products;
        }
    }

    // =========================
    // Electronic Product
    // =========================
    public class ElectronicProduct : IProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public Category Category => Category.Electronics;
        public int WarrantyMonths { get; set; }
        public string? Brand { get; set; }
    }

    // =========================
    // Discount Wrapper
    // =========================
    public class DiscountedProduct<T> where T : IProduct
    {
        private T _product;
        private decimal _discountPercentage;

        public DiscountedProduct(T product, decimal discountPercentage)
        {
            if (product == null)
                throw new ArgumentNullException("Product cannot be null");

            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount must be between 0 and 100");

            _product = product;
            _discountPercentage = discountPercentage;
        }

        // Calculate discounted price
        public decimal DiscountedPrice =>
            _product.Price * (1 - _discountPercentage / 100);

        public override string ToString()
        {
            return $"{_product.Name} | Original: {_product.Price} | " +
                   $"Discount: {_discountPercentage}% | Final: {DiscountedPrice}";
        }
    }

    // =========================
    // Inventory Manager
    // =========================
    public class InventoryManager
    {
        // Process any product collection
        public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
        {
            Console.WriteLine("\n--- Product List ---");
            foreach (var product in products)
                Console.WriteLine($"{product.Name} - {product.Price}");

            var expensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
            if (expensive != null)
                Console.WriteLine($"Most Expensive: {expensive.Name}");

            Console.WriteLine("\n--- Grouped by Category ---");
            foreach (var group in products.GroupBy(p => p.Category))
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                    Console.WriteLine($"  {item.Name}");
            }

            Console.WriteLine("\n--- Discounted Electronics (>500) ---");
            foreach (var p in products
                .Where(p => p.Category == Category.Electronics && p.Price > 500))
            {
                var discounted = new DiscountedProduct<T>(p, 10);
                Console.WriteLine(discounted);
            }
        }

        // Bulk price update
        public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
            where T : IProduct
        {
            foreach (var product in products)
            {
                try
                {
                    decimal newPrice = priceAdjuster(product);
                    if (newPrice <= 0)
                        throw new Exception("Invalid price");

                    product.Price = newPrice;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to update {product.Name}: {ex.Message}");
                }
            }
        }
    }

    // =========================
    // QUESTION CLASS
    // =========================
    public class Program
    {
        public static void Main()
        {
        var repository = new ProductRepository<ElectronicProduct>();
        var manager = new InventoryManager();
        bool exit = false;

        // =========================
        // DEFAULT HARDCODED PRODUCTS
        // =========================
        repository.AddProduct(new ElectronicProduct
        {
            Id = 1,
            Name = "Laptop",
            Price = 75000,
            Brand = "Dell",
            WarrantyMonths = 24
        });

        repository.AddProduct(new ElectronicProduct
        {
            Id = 2,
            Name = "Smartphone",
            Price = 45000,
            Brand = "Samsung",
            WarrantyMonths = 12
        });

        repository.AddProduct(new ElectronicProduct
        {
            Id = 3,
            Name = "Headphones",
            Price = 3000,
            Brand = "Sony",
            WarrantyMonths = 6
        });

        while (!exit)
        {
            Console.WriteLine("\n1. Add Electronic Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Find Product by Brand");
            Console.WriteLine("4. Process Inventory");
            Console.WriteLine("5. Update Prices (+10%)");
            Console.WriteLine("6. Total Inventory Value");
            Console.WriteLine("7. Exit");

            Console.Write("Enter choice: ");
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Id: ");
                        string? idInput = Console.ReadLine();
                        if (idInput == null || string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int id))
                        {
                            Console.WriteLine("Invalid Id input.");
                            break;
                        }

                        Console.Write("Name: ");
                        string? nameInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nameInput))
                        {
                            Console.WriteLine("Invalid Name input.");
                            break;
                        }
                        string name = nameInput;

                        Console.Write("Price: ");
                        string? priceInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(priceInput) || !decimal.TryParse(priceInput, out decimal price))
                        {
                            Console.WriteLine("Invalid Price input.");
                            break;
                        }

                        Console.Write("Brand: ");
                        string? brandInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(brandInput))
                        {
                            Console.WriteLine("Invalid Brand input.");
                            break;
                        }
                        string brand = brandInput;

                        Console.Write("Warranty Months: ");
                        string? warrantyInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(warrantyInput) || !int.TryParse(warrantyInput, out int warranty))
                        {
                            Console.WriteLine("Invalid Warranty Months input.");
                            break;
                        }

                        repository.AddProduct(new ElectronicProduct
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Brand = brand,
                            WarrantyMonths = warranty
                        });

                        Console.WriteLine("Product added successfully");
                        break;

                    case "2":
                        foreach (var p in repository.GetAllProducts())
                            Console.WriteLine($"{p.Id} - {p.Name} - {p.Price}");
                        break;

                    case "3":
                        Console.Write("Enter brand: ");
                        string? searchBrandInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(searchBrandInput))
                        {
                            Console.WriteLine("Invalid Brand input.");
                            break;
                        }
                        string searchBrand = searchBrandInput;

                        var results = repository.FindProducts(p => p.Brand == searchBrand);
                        foreach (var p in results)
                            Console.WriteLine($"{p.Name} - {p.Price}");
                        break;

                    case "4":
                        manager.ProcessProducts(repository.GetAllProducts());
                        break;

                    case "5":
                        manager.UpdatePrices(
                            repository.GetAllProducts(),
                            p => p.Price * 1.10m
                        );
                        Console.WriteLine("Prices updated");
                        break;

                    case "6":
                        Console.WriteLine("Total Value: " +
                            repository.CalculateTotalValue());
                        break;

                    case "7":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }       
    }
}   
