using System;
using System.Collections.Generic;
using System.Linq;

public interface IProduct
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; }
    Category Category { get; }
}

public enum Category { Electronics, Clothing, Books, Groceries }

// 1. Create a generic repository for products
public class ProductRepository<T> where T : class, IProduct
{
    private List<T> _products = new List<T>();

    // TODO: Implement method to add product with validation
    public void AddProduct(T product)
    {
        // Rule: Product ID must be unique
        // Rule: Price must be positive
        // Rule: Name cannot be null or empty
        // Add to collection if validation passes
        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        if (_products.Any(p => p.Id == product.Id))
            throw new ArgumentException("Product ID must be unique.", nameof(product));
        if (product.Price <= 0)
            throw new ArgumentException("Product price must be positive.", nameof(product));
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be null or empty.", nameof(product));

        _products.Add(product);
    }

    // TODO: Create method to find products by predicate
    public IEnumerable<T> FindProducts(Func<T, bool> predicate)
    {
        // Should return filtered products
        return _products.Where(predicate);

    }

    // TODO: Calculate total inventory value
    public decimal CalculateTotalValue()
    {
        // Return sum of all product prices
        return _products.Sum(p => p.Price);
    }

    public IEnumerable<T> GetAllProducts()
    {
        return _products;
    }
}

// 2. Specialized electronic product
public class ElectronicProduct : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category => Category.Electronics;
    public int WarrantyMonths { get; set; }
    public string Brand { get; set; }
}

// 3. Create a discounted product wrapper
public class DiscountedProduct<T> where T : IProduct
{
    private T _product;
    private decimal _discountPercentage;

    public DiscountedProduct(T product, decimal discountPercentage)
    {
        // TODO: Initialize with validation
        // Discount must be between 0 and 100
        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100.");

        _product = product;
        _discountPercentage = discountPercentage;
    }

    // TODO: Implement calculated price with discount
    public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);

    // TODO: Override ToString to show discount details
    public override string ToString()
    {
        return $"{_product.Name} - Original Price: {_product.Price:C}, Discount: {_discountPercentage}%, Discounted Price: {DiscountedPrice:C}";
    }
}

// 4. Inventory manager with constraints
public class InventoryManager
{
    // TODO: Create method that accepts any IProduct collection
    public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
    {
        // a) Print all product names and prices
        // b) Find the most expensive product
        // c) Group products by category
        // d) Apply 10% discount to Electronics over $500
        Console.WriteLine("Product List:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name} - {product.Price:C}");
        }
        var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
        Console.WriteLine($"Most expensive product: {mostExpensive?.Name} - {mostExpensive?.Price:C}");

        Console.WriteLine("Products grouped by category:");
        var grouped = products.GroupBy(p => p.Category);
        foreach (var group in grouped)
        {
            Console.WriteLine($"Category: {group.Key}");
            foreach (var product in group)
            {
                Console.WriteLine($"  {product.Name} - {product.Price:C}");
            }
        }
           
        Console.WriteLine("Applying 10% discount to Electronics over $500:");
        var discountedElectronics = products.Where(p => p.Category == Category.Electronics && p.Price > 500)
                                            .Select(p => new DiscountedProduct<IProduct>(p, 10));
        foreach (var discounted in discountedElectronics)
        {
            Console.WriteLine(discounted);
        }
    }

    // TODO: Implement bulk price update with delegate
    public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
        where T : IProduct
    {
        // Apply priceAdjuster to each product
        // Handle exceptions gracefully
        for (int i = 0; i < products.Count; i++)
        {
            try
            {
                var newPrice = priceAdjuster(products[i]);
                if (newPrice <= 0)
                    throw new ArgumentException("Adjusted price must be positive.");

                var prop = products[i].GetType().GetProperty("Price");
                prop?.SetValue(products[i], newPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating price for {products[i].Name}: {ex.Message}");
            }
        }
    }
}

class InventorySystem
{
    static void Main()
    {
        var repo = new ProductRepository<ElectronicProduct>();

        repo.AddProduct(new ElectronicProduct
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200,
            Brand = "Dell",
            WarrantyMonths = 24
        });

        repo.AddProduct(new ElectronicProduct
        {
            Id = 2,
            Name = "Headphones",
            Price = 150,
            Brand = "Sony",
            WarrantyMonths = 12
        });

        repo.AddProduct(new ElectronicProduct
        {
            Id = 3,
            Name = "Smart TV",
            Price = 900,
            Brand = "Samsung",
            WarrantyMonths = 36
        });

        Console.WriteLine("\nTotal Inventory Value:");
        Console.WriteLine(repo.CalculateTotalValue());

        // Find by brand
        Console.WriteLine("\nProducts by Samsung:");
        var samsung = repo.FindProducts(p => p.Brand == "Samsung");
        foreach (var p in samsung)
            Console.WriteLine(p.Name);

        // Process inventory
        var manager = new InventoryManager();
        manager.ProcessProducts(repo.GetAllProducts().ToList());

        // Bulk price update (increase 5%)
        manager.UpdatePrices(repo.GetAllProducts().ToList(), p => p.Price * 1.05m);

        Console.WriteLine("\nAfter Price Update:");
        manager.ProcessProducts(repo.GetAllProducts().ToList());
    }
}