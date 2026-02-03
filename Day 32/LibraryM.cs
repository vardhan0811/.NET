using System;
using System.Collections.Generic;
using System.Linq;

namespace Day32
{
    // Represents a book in the library
    public class Book
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }

        // Constructor to initialize a book
        public Book(int id, string title, string author, string genre, int publicationYear)
        {
            Id = id;
            Title = title ?? string.Empty;
            Author = author ?? string.Empty;
            Genre = genre ?? string.Empty;
            PublicationYear = publicationYear;
        }

        // Returns a string representation of the book
        public override string ToString()
        {
            return $"[{Id}] \"{Title}\" by {Author} ({PublicationYear}) - {Genre}";
        }
    }

    // Utility class for managing the library
    public class LibraryUtility
    {
        private readonly List<Book> _books = new List<Book>();
        private int _nextId = 1;

        // Adds a new book to the library
        public void AddBook(string title, string author, string genre, int year)
        {
            var book = new Book(_nextId++, title, author, genre, year);
            _books.Add(book);
        }

        // Groups books by genre and sorts each group by title
        public SortedDictionary<string, List<Book>> GroupBooksByGenre()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var result = new SortedDictionary<string, List<Book>>(comparer);

            foreach (var book in _books)
            {
                var key = book.Genre ?? string.Empty;
                if (!result.TryGetValue(key, out var list))
                {
                    list = new List<Book>();
                    result[key] = list;
                }
                list.Add(book);
            }

            // Sort each genre group by book title
            foreach (var key in result.Keys.ToList())
            {
                result[key] = result[key].OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase).ToList();
            }

            return result;
        }

        // Returns a list of books by the specified author, sorted by year and title
        public List<Book> GetBooksByAuthor(string author)
        {
            if (author == null) return new List<Book>();
            return _books
                .Where(b => string.Equals(b.Author, author, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.PublicationYear)
                .ThenBy(b => b.Title, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        // Returns the total number of books in the library
        public int GetTotalBooksCount()
        {
            return _books.Count;
        }
    }

    // Main class for the library management console app
    class LibraryM
    {
        // Entry point for the library menu
        static void Run()
        {
            var library = new LibraryUtility();

            // Seed data
            library.AddBook("1984", "George Orwell", "Fiction", 1949);
            library.AddBook("Sapiens", "Yuval Noah Harari", "Non-Fiction", 2011);
            library.AddBook("The Hound of the Baskervilles", "Arthur Conan Doyle", "Mystery", 1902);
            library.AddBook("Animal Farm", "George Orwell", "Fiction", 1945);
            library.AddBook("Thinking, Fast and Slow", "Daniel Kahneman", "Non-Fiction", 2011);

            // Main menu loop
            while (true)
            {
                Console.WriteLine("\n--- Library Menu ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Display Books Grouped by Genre");
                Console.WriteLine("3. Search Books by Author");
                Console.WriteLine("4. Show Statistics");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option (1-5): ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddBookUI(library);
                        break;
                    case "2":
                        DisplayBooksByGenreUI(library);
                        break;
                    case "3":
                        SearchBooksByAuthorUI(library);
                        break;
                    case "4":
                        ShowStatisticsUI(library);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        // UI for adding a new book
        static void AddBookUI(LibraryUtility library)
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Author: ");
            var author = Console.ReadLine();
            Console.Write("Genre: ");
            var genre = Console.ReadLine();
            Console.Write("Publication Year: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year.");
                return;
            }
            library.AddBook(title ?? string.Empty, author ?? string.Empty, genre ?? string.Empty, year);
            Console.WriteLine("Book added.");
        }

        // UI for displaying books grouped by genre
        static void DisplayBooksByGenreUI(LibraryUtility library)
        {
            var grouped = library.GroupBooksByGenre();
            Console.WriteLine("\nBooks grouped by genre:");
            foreach (var kvp in grouped)
            {
                Console.WriteLine($"\nGenre: {kvp.Key} ({kvp.Value.Count} book(s))");
                foreach (var book in kvp.Value)
                    Console.WriteLine("  " + book);
            }
        }

        // UI for searching books by author
        static void SearchBooksByAuthorUI(LibraryUtility library)
        {
            Console.Write("Enter author name: ");
            var author = Console.ReadLine();
            var books = library.GetBooksByAuthor(author ?? string.Empty);
            if (books.Count == 0)
            {
                Console.WriteLine("No books found for this author.");
            }
            else
            {
                Console.WriteLine($"\nBooks by {author}:");
                foreach (var book in books)
                    Console.WriteLine("  " + book);
            }
        }

        // UI for showing library statistics
        static void ShowStatisticsUI(LibraryUtility library)
        {
            var grouped = library.GroupBooksByGenre();
            Console.WriteLine($"\nTotal books: {library.GetTotalBooksCount()}");
            Console.WriteLine("Books per genre:");
            foreach (var kvp in grouped)
                Console.WriteLine($"  {kvp.Key}: {kvp.Value.Count}");
        }
    }
}