using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
// Represents a single menu item in the restaurant
public class MenuItem
{
    public string ItemName { get; set; }
    public string Category { get; set; } // Appetizer/Main Course/Dessert
    public double Price { get; set; }
    public bool IsVegetarian { get; set; }

    // Constructor to initialize a menu item
    public MenuItem(string itemName, string category, double price, bool isVegetarian)
    {
        ItemName = itemName;
        Category = category;
        Price = price;
        IsVegetarian = isVegetarian;
    }
}

// Manages the restaurant menu and related operations
public class MenuManager
{
    private List<MenuItem> menuItems = new List<MenuItem>();

    // Adds a new menu item after validating input
    public void AddMenuItem(string name, string category, double price, bool isVeg)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name cannot be empty.");
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be empty.");
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        menuItems.Add(new MenuItem(name, category, price, isVeg));
    }

    // Groups menu items by their category
    public Dictionary<string, List<MenuItem>> GroupItemsByCategory()
    {
        return menuItems
            .GroupBy(item => item.Category)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    // Returns a list of vegetarian menu items
    public List<MenuItem> GetVegetarianItems()
    {
        return menuItems.Where(item => item.IsVegetarian).ToList();
    }

    // Calculates the average price for a given category
    public double CalculateAveragePriceByCategory(string category)
    {
        var itemsInCategory = menuItems.Where(item => item.Category == category).ToList();
        if (itemsInCategory.Count == 0) return 0;
        return itemsInCategory.Average(item => item.Price);
    }

    // Returns all menu items
    public List<MenuItem> GetAllItems()
    {
        return menuItems;
    }
}

// Main class for the Restaurant Menu application
class RestaurantM
{
    static void Run()
    {
        var manager = new MenuManager();
        // Sample data
        manager.AddMenuItem("Spring Rolls", "Appetizer", 5.99, true);
        manager.AddMenuItem("Chicken Curry", "Main Course", 12.99, false);
        manager.AddMenuItem("Veggie Burger", "Main Course", 10.99, true);
        manager.AddMenuItem("Ice Cream", "Dessert", 4.99, true);

        // Main menu loop
        while (true)
        {
            Console.WriteLine("\n--- Restaurant Menu Manager ---");
            Console.WriteLine("1. Display menu by category");
            Console.WriteLine("2. Show vegetarian-only menu");
            Console.WriteLine("3. Calculate average prices per category");
            Console.WriteLine("4. Add new menu item");
            Console.WriteLine("5. Show all menu items");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayMenuByCategory(manager);
                    break;
                case "2":
                    DisplayVegetarianMenu(manager);
                    break;
                case "3":
                    DisplayAveragePrices(manager);
                    break;
                case "4":
                    AddMenuItemUI(manager);
                    break;
                case "5":
                    DisplayAllMenuItems(manager);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    // Displays menu items grouped by category
    static void DisplayMenuByCategory(MenuManager manager)
    {
        var grouped = manager.GroupItemsByCategory();
        foreach (var category in grouped.Keys)
        {
            Console.WriteLine($"{category}:");
            foreach (var item in grouped[category])
            {
                Console.WriteLine($"  {item.ItemName} - ${item.Price} {(item.IsVegetarian ? "(V)" : "")}");
            }
        }
    }

    // Displays only vegetarian menu items
    static void DisplayVegetarianMenu(MenuManager manager)
    {
        Console.WriteLine("\nVegetarian Menu:");
        foreach (var item in manager.GetVegetarianItems())
        {
            Console.WriteLine($"{item.ItemName} - ${item.Price} ({item.Category})");
        }
    }

    // Displays average prices for each category
    static void DisplayAveragePrices(MenuManager manager)
    {
        var grouped = manager.GroupItemsByCategory();
        Console.WriteLine("\nAverage Prices:");
        foreach (var category in grouped.Keys)
        {
            double avg = manager.CalculateAveragePriceByCategory(category);
            Console.WriteLine($"{category}: ${avg:F2}");
        }
    }

    // UI for adding a new menu item
    static void AddMenuItemUI(MenuManager manager)
    {
        try
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();
            Console.Write("Enter category (Appetizer/Main Course/Dessert): ");
            string category = Console.ReadLine();
            Console.Write("Enter price: ");
            double price = double.Parse(Console.ReadLine());
            Console.Write("Is vegetarian? (y/n): ");
            bool isVeg = Console.ReadLine().Trim().ToLower() == "y";
            manager.AddMenuItem(name, category, price, isVeg);
            Console.WriteLine("Menu item added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Displays all menu items
    static void DisplayAllMenuItems(MenuManager manager)
    {
        Console.WriteLine("\nAll Menu Items:");
        foreach (var item in manager.GetAllItems())
        {
            Console.WriteLine($"{item.ItemName} - ${item.Price} ({item.Category}) {(item.IsVegetarian ? "(V)" : "")}");
        }
    }
}
}