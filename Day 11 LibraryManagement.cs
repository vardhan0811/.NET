using System; // Import System namespace
using ItemAlias = LibrarySystem.Items; // Alias for LibrarySystem.Items namespace
using LibrarySystem; // Import LibrarySystem namespace


namespace LibrarySystem // Define LibrarySystem namespace
{
    // TASK 7: Enumerations
    public enum UserRole { Admin, Librarian, Member } // Enum for user roles
    public enum ItemStatus { Available, Borrowed, Reserved, Lost } // Enum for item status

    // TASK 1: Abstract Class & Abstract Methods
    public abstract class LibraryItem // Abstract base class for library items
    {
        public string Title { get; set; } // Title property
        public string Author { get; set; } // Author property
        public int ItemID { get; set; } // Item ID property
        public ItemStatus Status { get; set; } // Status property

        public LibraryItem(string title, string author, int itemId) // Constructor
        {
            Title = title; // Set title
            Author = author; // Set author
            ItemID = itemId; // Set item ID
            Status = ItemStatus.Available; // Set status to available
        }

        public abstract void DisplayItemDetails(); // Abstract method to display details
        public abstract double CalculateLateFee(int days); // Abstract method to calculate late fee
    }

    // TASK 2: Interfaces
    public interface IReservable // Interface for reservable items
    {
        void Reserve(); // Reserve method
    }
    public interface INotifiable // Interface for notifiable items
    {
        void Notify(string message); // Notify method
    }

    // TASK 5: Namespaces & Nested Namespaces
    namespace Items // Nested namespace for items
    {
        // TASK 4: Explicit Interface Implementation
        public class Book : LibraryItem, IReservable, INotifiable // Book class
        {
            public Book(string title, string author, int itemId) : base(title, author, itemId) { } // Constructor

            public override void DisplayItemDetails() // Override to display book details
            {
                Console.WriteLine("Item Type: Book"); // Print item type
                Console.WriteLine($"Title: {Title}"); // Print title
                Console.WriteLine($"Author: {Author}"); // Print author
                Console.WriteLine($"Item ID: {ItemID}"); // Print item ID
            }

            public override double CalculateLateFee(int days) // Override to calculate late fee
            {
                return days * 1.0; // $1 per day
            }

            // Explicit interface implementation
            void IReservable.Reserve() // Reserve method
            {
                Console.WriteLine("Book reserved successfully."); // Print reservation message
            }
            void INotifiable.Notify(string message) // Notify method
            {
                Console.WriteLine($"Notification: {message}"); // Print notification
            }
        }

        public class Magazine : LibraryItem // Magazine class
        {
            public Magazine(string title, string author, int itemId) : base(title, author, itemId) { } // Constructor

            public override void DisplayItemDetails() // Override to display magazine details
            {
                Console.WriteLine("Item Type: Magazine"); // Print item type
                Console.WriteLine($"Title: {Title}"); // Print title
                Console.WriteLine($"Author: {Author}"); // Print author
                Console.WriteLine($"Item ID: {ItemID}"); // Print item ID
            }

            public override double CalculateLateFee(int days) // Override to calculate late fee
            {
                return days * 0.5; // $0.5 per day
            }
        }

        // BONUS TASK: eBook
        public class eBook : LibraryItem // eBook class
        {
            public eBook(string title, string author, int itemId) : base(title, author, itemId) { } // Constructor

            public override void DisplayItemDetails() // Override to display eBook details
            {
                Console.WriteLine("Item Type: eBook"); // Print item type
                Console.WriteLine($"Title: {Title}"); // Print title
                Console.WriteLine($"Author: {Author}"); // Print author
                Console.WriteLine($"Item ID: {ItemID}"); // Print item ID
            }

            public override double CalculateLateFee(int days) // Override to calculate late fee
            {
                return 0; // eBooks have no late fee
            }

            public void Download() // Download method
            {
                Console.WriteLine("eBook downloaded successfully."); // Print download message
            }
        }
    }

    namespace Users // Nested namespace for users
    {
        public class Member // Member class
        {
            public string Name { get; set; } // Name property
            public UserRole Role { get; set; } // Role property

            public Member(string name, UserRole role) // Constructor
            {
                Name = name; // Set name
                Role = role; // Set role
            }
        }
    }

    // TASK 6: Partial & Static Classes
    public partial class LibraryAnalytics // Partial class for analytics
    {
        public static int TotalBorrowedItems { get; set; } // Static property for total borrowed
        public static void DisplayAnalytics() // Static method to display analytics
        {
            Console.WriteLine($"Total Items Borrowed: {TotalBorrowedItems}"); // Print total borrowed
        }
    }
    public partial class LibraryAnalytics // Partial class continuation
    {
        // Additional analytics methods can be added here
    }
}

// Namespace alias

public class Day11LibraryManagement // Main class for Day 11
{
    public static void Run() // Main method to run library management demo
    {
        // TASK 1: Abstract Class & Abstract Methods
        var book = new ItemAlias.Book("C# Fundamentals", "John Doe", 101); // Create a Book object
        var magazine = new ItemAlias.Magazine("Tech Today", "Jane Doe", 201); // Create a Magazine object

        book.DisplayItemDetails(); // Display book details
        Console.WriteLine($"Late Fee for 3 days: {book.CalculateLateFee(3)}"); // Show late fee for book
        magazine.DisplayItemDetails(); // Display magazine details
        Console.WriteLine($"Late Fee for 3 days: {magazine.CalculateLateFee(3)}"); // Show late fee for magazine

        // TASK 2: Interfaces & Multiple Inheritance
        IReservable reservableBook = book; // Cast book to IReservable
        reservableBook.Reserve(); // Reserve the book
        INotifiable notifiableBook = book; // Cast book to INotifiable
        notifiableBook.Notify("Your reserved book is ready for pickup."); // Notify user

        // TASK 3: Dynamic Polymorphism
        var items = new System.Collections.Generic.List<LibrarySystem.LibraryItem> // Create a list of LibraryItem
        {
            book, // Add book
            magazine // Add magazine
        };
        foreach (var item in items) // Iterate through items
        {
            item.DisplayItemDetails(); // Display details for each item
        }
        // Explanation:
        Console.WriteLine("The method executed depends on the object type at runtime, not the reference type."); // Explain dynamic polymorphism

        // TASK 4: Explicit Interface Implementation
        IReservable resBook = book; // Cast book to IReservable
        resBook.Reserve(); // Reserve the book
        INotifiable notifBook = book; // Cast book to INotifiable
        notifBook.Notify("Please return the book on time."); // Notify user
        // Explanation:
        Console.WriteLine("Explicit implementation prevents direct access and exposes functionality only through interfaces."); // Explain explicit interface implementation

        // TASK 5: Namespaces & Nested Namespaces
        var aliasBook = new ItemAlias.Book("Alias Book", "Alias Author", 301); // Create book using namespace alias
        var aliasMagazine = new ItemAlias.Magazine("Alias Magazine", "Alias Editor", 401); // Create magazine using namespace alias
        Console.WriteLine("Book and Magazine objects created using namespace alias."); // Print message
        // Explanation:
        Console.WriteLine("1. Nested namespaces organize large projects logically."); // Explain nested namespaces
        Console.WriteLine("2. Aliases reduce long namespace references and improve readability."); // Explain aliases

        // TASK 6: Partial & Static Classes
        LibrarySystem.LibraryAnalytics.TotalBorrowedItems = 5; // Set total borrowed items
        LibrarySystem.LibraryAnalytics.DisplayAnalytics(); // Display analytics
        // Explanation:
        Console.WriteLine("Static members store system-wide data shared across all objects."); // Explain static members

        // TASK 7: Enumerations (Enums)
        var member = new LibrarySystem.Users.Member("Alice", LibrarySystem.UserRole.Member); // Create a Member object
        book.Status = LibrarySystem.ItemStatus.Borrowed; // Set book status
        Console.WriteLine($"User Role: {member.Role}"); // Print user role
        Console.WriteLine($"Item Status: {book.Status}"); // Print item status
        // Explanation:
        Console.WriteLine("Enums prevent invalid values and improve code readability."); // Explain enums

        // BONUS TASK: Notification by role & eBook
        var admin = new LibrarySystem.Users.Member("Bob", LibrarySystem.UserRole.Admin); // Create admin member
        NotifyByRole(admin); // Notify admin
        NotifyByRole(member); // Notify member

        var ebook = new ItemAlias.eBook("AI Revolution", "Dr. Smith", 501); // Create eBook object
        ebook.DisplayItemDetails(); // Display eBook details
        ebook.Download(); // Download eBook
    }

    // BONUS TASK: Notification logic by role
    public static void NotifyByRole(LibrarySystem.Users.Member user) // Notify user based on role
    {
        if (user.Role == LibrarySystem.UserRole.Admin) // If user is admin
            Console.WriteLine("Admin Alert: System maintenance scheduled."); // Print admin alert
        else if (user.Role == LibrarySystem.UserRole.Member) // If user is member
            Console.WriteLine("Member Notification: Your borrowed item is due tomorrow."); // Print member notification
        else
            Console.WriteLine("Notification: General library update."); // Print general notification
    }
}