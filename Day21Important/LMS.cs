using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21Important
{
    public interface IBook
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Category { get; set; }
        int Price { get; set; }
    }

    public interface ILibrarySystem
    {
        void AddBook(IBook book, int quantity);
        void RemoveBook(IBook bookId, int quantity);
        int CalculateTotal();
        void CategoryTotalPrice();
        void BooksInfo();
        void CategoryAndAuthorWithCount();
    }

    public class Book : IBook
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int Price { get; set; }
    }

    public class LibrarySystem : ILibrarySystem
    {
        private Dictionary<IBook, int> _books = new Dictionary<IBook, int>();

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
                _books.Add(book);
            }
        }

        public void RemoveBook(IBook bookId, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                _books.Remove(bookId);
            }
        }

        public int CalculateTotal()
        {
            return _books.Sum(book => book.Price);
        }

        public void CategoryTotalPrice()
        {
            var categoryTotal = _books.GroupBy(book => book.Category)
                .Select(group => new
                {
                    Category = group.Key,
                    TotalPrice = group.Sum(book => book.Price)
                });

            foreach (var item in categoryTotal)
            {
                Console.WriteLine($"Category: {item.Category}, Total Price: {item.TotalPrice}");
            }
        }

        public void BooksInfo()
        {
            foreach (var book in _books)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Category: {book.Category}, Price: {book.Price}");
            }
        }

        public void CategoryAndAuthorWithCount()
        {
            var result = _books.GroupBy(book => new { book.Category, book.Author })
                .Select(group => new
                {
                    Category = group.Key.Category,
                    Author = group.Key.Author,
                    Count = group.Count()
                });

            foreach (var item in result)
            {
                Console.WriteLine($"Category: {item.Category}, Author: {item.Author}, Count: {item.Count}");
            }
        }
    }
}