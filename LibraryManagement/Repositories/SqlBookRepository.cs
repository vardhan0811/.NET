using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Repositories
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly LibraryDbContext context;

        public SqlBookRepository(LibraryDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public void AddBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.BookId == id);

            if (book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }
    }
}