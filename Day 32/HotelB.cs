using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
// Represents a hotel room with properties for number, type, price, and availability
public class Room
{
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public double PricePerNight { get; set; }
    public bool IsAvailable { get; set; }

    public Room(int roomNumber, string roomType, double pricePerNight)
    {
        RoomNumber = roomNumber;
        RoomType = roomType;
        PricePerNight = pricePerNight;
        IsAvailable = true; // All rooms start as available
    }
}

// Manages hotel rooms and booking logic
public class HotelManager
{
    private List<Room> rooms = new List<Room>();

    // Adds a new room if the room number is unique
    public void AddRoom(int roomNumber, string type, double price)
    {
        if (rooms.Any(r => r.RoomNumber == roomNumber))
            return;
        rooms.Add(new Room(roomNumber, type, price));
    }

    // Groups available rooms by their type
    public Dictionary<string, List<Room>> GroupRoomsByType()
    {
        return rooms
            .Where(r => r.IsAvailable)
            .GroupBy(r => r.RoomType)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    // Books a room if available, marks it as unavailable, and prints total cost
    public bool BookRoom(int roomNumber, int nights)
    {
        var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber && r.IsAvailable);
        if (room == null)
            return false;
        room.IsAvailable = false;
        double totalCost = room.PricePerNight * nights;
        Console.WriteLine($"Room {roomNumber} booked for {nights} nights. Total cost: {totalCost:C}");
        return true;
    }

    // Returns available rooms within a specified price range
    public List<Room> GetAvailableRoomsByPriceRange(double min, double max)
    {
        return rooms
            .Where(r => r.IsAvailable && r.PricePerNight >= min && r.PricePerNight <= max)
            .ToList();
    }

    // Returns all rooms (available and booked)
    public List<Room> GetAllRooms()
    {
        return rooms;
    }
}

// Main class for hotel application
class HotelB
{
    // Entry point for the hotel management menu
    static void Run()
    {
        HotelManager manager = new HotelManager();
        // Add some sample rooms
        manager.AddRoom(101, "Single", 50);
        manager.AddRoom(102, "Double", 80);
        manager.AddRoom(103, "Suite", 150);
        manager.AddRoom(104, "Single", 55);

        while (true)
        {
            // Display menu options
            Console.WriteLine("\n--- Hotel Menu ---");
            Console.WriteLine("1. Show available rooms grouped by type");
            Console.WriteLine("2. Book a room");
            Console.WriteLine("3. Show rooms within price range");
            Console.WriteLine("4. Show all rooms");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowGroupedRooms(manager);
                    break;
                case "2":
                    BookRoomUI(manager);
                    break;
                case "3":
                    ShowRoomsByPriceRange(manager);
                    break;
                case "4":
                    ShowAllRooms(manager);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    // Displays available rooms grouped by their type
    static void ShowGroupedRooms(HotelManager manager)
    {
        Console.WriteLine("Available rooms grouped by type:");
        var grouped = manager.GroupRoomsByType();
        foreach (var type in grouped.Keys)
        {
            Console.WriteLine($"{type}:");
            foreach (var room in grouped[type])
                Console.WriteLine($"  Room {room.RoomNumber}, Price: {room.PricePerNight}");
        }
    }

    // Handles user input for booking a room
    static void BookRoomUI(HotelManager manager)
    {
        Console.Write("Enter room number to book: ");
        if (!int.TryParse(Console.ReadLine(), out int roomNumber))
        {
            Console.WriteLine("Invalid room number.");
            return;
        }
        Console.Write("Enter number of nights: ");
        if (!int.TryParse(Console.ReadLine(), out int nights))
        {
            Console.WriteLine("Invalid number of nights.");
            return;
        }
        if (!manager.BookRoom(roomNumber, nights))
            Console.WriteLine("Room not available or does not exist.");
    }

    // Shows available rooms within a user-specified price range
    static void ShowRoomsByPriceRange(HotelManager manager)
    {
        Console.Write("Enter minimum price: ");
        if (!double.TryParse(Console.ReadLine(), out double min))
        {
            Console.WriteLine("Invalid price.");
            return;
        }
        Console.Write("Enter maximum price: ");
        if (!double.TryParse(Console.ReadLine(), out double max))
        {
            Console.WriteLine("Invalid price.");
            return;
        }
        var budgetRooms = manager.GetAvailableRoomsByPriceRange(min, max);
        if (budgetRooms.Count == 0)
        {
            Console.WriteLine("No rooms found in this price range.");
            return;
        }
        foreach (var room in budgetRooms)
            Console.WriteLine($"Room {room.RoomNumber}, Type: {room.RoomType}, Price: {room.PricePerNight}");
    }

    // Shows all rooms and their status (available/booked)
    static void ShowAllRooms(HotelManager manager)
    {
        var rooms = manager.GetAllRooms();
        foreach (var room in rooms)
        {
            string status = room.IsAvailable ? "Available" : "Booked";
            Console.WriteLine($"Room {room.RoomNumber}, Type: {room.RoomType}, Price: {room.PricePerNight}, Status: {status}");
        }
    }
}
}