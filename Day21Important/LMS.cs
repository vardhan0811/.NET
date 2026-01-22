using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21Important
{
    /// <summary>
    /// Interface representing a book with basic properties.
    /// </summary>
    public interface IBook
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Category { get; set; }
        int Price { get; set; }
    }

    /// <summary>
    /// Interface for library system operations.
    /// </summary>
    public interface ILibrarySystem
    {
        void AddBook(IBook book, int quantity);
        void RemoveBook(IBook bookId, int quantity);
        int CalculateTotal();
        void CategoryTotalPrice();
        void BooksInfo();
        void CategoryAndAuthorWithCount();
    }

    /// <summary>
    /// Represents a book with properties for library management.
    /// </summary>
    public class Book : IBook
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int Price { get; set; }
    }

    /// <summary>
    /// Implements the library system for managing books and their quantities.
    /// </summary>
    public class LibrarySystem : ILibrarySystem
    {
        private Dictionary<IBook, int> _books = new Dictionary<IBook, int>();

        /// <summary>
        /// Adds a book to the library or increases its quantity.
        /// </summary>
        public void AddBook(IBook book, int quantity)
        {
            if (_books.ContainsKey(book))
            {
                _books[book] += quantity;
            }
            else
            {
                _books[book] = quantity;
            }
        }

        /// <summary>
        /// Removes a specified quantity of a book from the library.
        /// </summary>
        public void RemoveBook(IBook book, int quantity)
        {
            if (_books.ContainsKey(book))
            {
                _books[book] -= quantity;
                if (_books[book] <= 0)
                {
                    _books.Remove(book);
                }
            }
        }

        /// <summary>
        /// Calculates the total price of all books in the library.
        /// </summary>
        public int CalculateTotal()
        {
            return _books.Sum(book => book.Key.Price * book.Value);
        }

        /// <summary>
        /// Displays the total price of books grouped by category.
        /// </summary>
        public void CategoryTotalPrice()
        {
            Console.WriteLine("Total Price by Category:");
            var categoryTotal = _books.GroupBy(book => book.Key.Category)
                .Select(group => new
                {
                    Category = group.Key,
                    TotalPrice = group.Sum(book => book.Key.Price * book.Value)
                });

            foreach (var item in categoryTotal)
            {
                Console.WriteLine($"Category: {item.Category}, Total Price: {item.TotalPrice}");
            }
        }

        /// <summary>
        /// Displays information about all books in the library.
        /// </summary>
        public void BooksInfo()
        {
            Console.WriteLine("Books Information:");
            foreach (var book in _books)
            {
                Console.WriteLine($"Title: {book.Key.Title}, Author: {book.Key.Author}, Category: {book.Key.Category}, Price: {book.Key.Price}, Quantity: {book.Value}");
            }
        }

        /// <summary>
        /// Displays the count of books grouped by category and author.
        /// </summary>
        public void CategoryAndAuthorWithCount()
        {
            var result = _books.GroupBy(book => new { book.Key.Category, book.Key.Author })
                .Select(group => new
                {
                    Category = group.Key.Category,
                    Author = group.Key.Author,
                    Count = group.Sum(book => book.Value)
                });

            foreach (var item in result)
            {
                Console.WriteLine($"Category: {item.Category}, Author: {item.Author}, Count: {item.Count}");
            }
        }
    }

    /// <summary>
    /// Entry point for the Library Management System (LMS).
    /// </summary>
    public class LMS
    {
        /// <summary>
        /// Runs the LMS by reading book data and displaying various reports.
        /// </summary>
        public static void Run()
        {
            ILibrarySystem library = new LibrarySystem();

            int n = int.Parse(Console.ReadLine()!);
            for (int i = 0; i < n; i++)
            {
                string[] bookInfo = Console.ReadLine()!.Split(',');
                IBook book = new Book
                {
                    Id = int.Parse(bookInfo[0]),
                    Title = bookInfo[1],
                    Author = bookInfo[2],
                    Category = bookInfo[3],
                    Price = int.Parse(bookInfo[4])
                };
                library.AddBook(book, int.Parse(bookInfo[5]));
            }

            Console.WriteLine($"Total Price of Books: {library.CalculateTotal()}");

            library.CategoryTotalPrice();
            library.BooksInfo();
            library.CategoryAndAuthorWithCount();
        }
    }
}