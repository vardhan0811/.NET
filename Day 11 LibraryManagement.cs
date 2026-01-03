using System;
using ItemAlias = LibrarySystem.Items;
using LibrarySystem;


namespace LibrarySystem
{
    // TASK 7: Enumerations
    public enum UserRole { Admin, Librarian, Member }
    public enum ItemStatus { Available, Borrowed, Reserved, Lost }

    // TASK 1: Abstract Class & Abstract Methods
    public abstract class LibraryItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int ItemID { get; set; }
        public ItemStatus Status { get; set; }

        public LibraryItem(string title, string author, int itemId)
        {
            Title = title;
            Author = author;
            ItemID = itemId;
            Status = ItemStatus.Available;
        }

        public abstract void DisplayItemDetails();
        public abstract double CalculateLateFee(int days);
    }

    // TASK 2: Interfaces
    public interface IReservable
    {
        void Reserve();
    }
    public interface INotifiable
    {
        void Notify(string message);
    }

    // TASK 5: Namespaces & Nested Namespaces
    namespace Items
    {
        // TASK 4: Explicit Interface Implementation
        public class Book : LibraryItem, IReservable, INotifiable
        {
            public Book(string title, string author, int itemId) : base(title, author, itemId) { }

            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: Book");
                Console.WriteLine($"Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"Item ID: {ItemID}");
            }

            public override double CalculateLateFee(int days)
            {
                return days * 1.0;
            }

            // Explicit interface implementation
            void IReservable.Reserve()
            {
                Console.WriteLine("Book reserved successfully.");
            }
            void INotifiable.Notify(string message)
            {
                Console.WriteLine($"Notification: {message}");
            }
        }

        public class Magazine : LibraryItem
        {
            public Magazine(string title, string author, int itemId) : base(title, author, itemId) { }

            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: Magazine");
                Console.WriteLine($"Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"Item ID: {ItemID}");
            }

            public override double CalculateLateFee(int days)
            {
                return days * 0.5;
            }
        }

        // BONUS TASK: eBook
        public class eBook : LibraryItem
        {
            public eBook(string title, string author, int itemId) : base(title, author, itemId) { }

            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: eBook");
                Console.WriteLine($"Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"Item ID: {ItemID}");
            }

            public override double CalculateLateFee(int days)
            {
                return 0; // eBooks have no late fee
            }

            public void Download()
            {
                Console.WriteLine("eBook downloaded successfully.");
            }
        }
    }

    namespace Users
    {
        public class Member
        {
            public string Name { get; set; }
            public UserRole Role { get; set; }

            public Member(string name, UserRole role)
            {
                Name = name;
                Role = role;
            }
        }
    }

    // TASK 6: Partial & Static Classes
    public partial class LibraryAnalytics
    {
        public static int TotalBorrowedItems { get; set; }
        public static void DisplayAnalytics()
        {
            Console.WriteLine($"Total Items Borrowed: {TotalBorrowedItems}");
        }
    }
    public partial class LibraryAnalytics
    {
        // Additional analytics methods can be added here
    }
}

// Namespace alias

public class Day11LibraryManagement
{
    public static void Run()
    {
        // TASK 1: Abstract Class & Abstract Methods
        var book = new ItemAlias.Book("C# Fundamentals", "John Doe", 101);
        var magazine = new ItemAlias.Magazine("Tech Today", "Jane Doe", 201);

        book.DisplayItemDetails();
        Console.WriteLine($"Late Fee for 3 days: {book.CalculateLateFee(3)}");
        magazine.DisplayItemDetails();
        Console.WriteLine($"Late Fee for 3 days: {magazine.CalculateLateFee(3)}");

        // TASK 2: Interfaces & Multiple Inheritance
        IReservable reservableBook = book;
        reservableBook.Reserve();
        INotifiable notifiableBook = book;
        notifiableBook.Notify("Your reserved book is ready for pickup.");

        // TASK 3: Dynamic Polymorphism
        var items = new System.Collections.Generic.List<LibrarySystem.LibraryItem>
        {
            book,
            magazine
        };
        foreach (var item in items)
        {
            item.DisplayItemDetails();
        }
        // Explanation:
        Console.WriteLine("The method executed depends on the object type at runtime, not the reference type.");

        // TASK 4: Explicit Interface Implementation
        IReservable resBook = book;
        resBook.Reserve();
        INotifiable notifBook = book;
        notifBook.Notify("Please return the book on time.");
        // Explanation:
        Console.WriteLine("Explicit implementation prevents direct access and exposes functionality only through interfaces.");

        // TASK 5: Namespaces & Nested Namespaces
        var aliasBook = new ItemAlias.Book("Alias Book", "Alias Author", 301);
        var aliasMagazine = new ItemAlias.Magazine("Alias Magazine", "Alias Editor", 401);
        Console.WriteLine("Book and Magazine objects created using namespace alias.");
        // Explanation:
        Console.WriteLine("1. Nested namespaces organize large projects logically.");
        Console.WriteLine("2. Aliases reduce long namespace references and improve readability.");

        // TASK 6: Partial & Static Classes
        LibrarySystem.LibraryAnalytics.TotalBorrowedItems = 5;
        LibrarySystem.LibraryAnalytics.DisplayAnalytics();
        // Explanation:
        Console.WriteLine("Static members store system-wide data shared across all objects.");

        // TASK 7: Enumerations (Enums)
        var member = new LibrarySystem.Users.Member("Alice", LibrarySystem.UserRole.Member);
        book.Status = LibrarySystem.ItemStatus.Borrowed;
        Console.WriteLine($"User Role: {member.Role}");
        Console.WriteLine($"Item Status: {book.Status}");
        // Explanation:
        Console.WriteLine("Enums prevent invalid values and improve code readability.");

        // BONUS TASK: Notification by role & eBook
        var admin = new LibrarySystem.Users.Member("Bob", LibrarySystem.UserRole.Admin);
        NotifyByRole(admin);
        NotifyByRole(member);

        var ebook = new ItemAlias.eBook("AI Revolution", "Dr. Smith", 501);
        ebook.DisplayItemDetails();
        ebook.Download();
    }

    // BONUS TASK: Notification logic by role
    public static void NotifyByRole(LibrarySystem.Users.Member user)
    {
        if (user.Role == LibrarySystem.UserRole.Admin)
            Console.WriteLine("Admin Alert: System maintenance scheduled.");
        else if (user.Role == LibrarySystem.UserRole.Member)
            Console.WriteLine("Member Notification: Your borrowed item is due tomorrow.");
        else
            Console.WriteLine("Notification: General library update.");
    }
}