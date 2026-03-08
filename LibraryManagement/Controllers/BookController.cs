using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Repositories;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository repo;

        public BookController(IBookRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult List()
        {
            var books = repo.GetAllBooks();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = repo.GetBookById(id);
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            repo.AddBook(book);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            repo.DeleteBook(id);
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            repo.DeleteBook(book.BookId);
            return RedirectToAction("List");
        }
    }
}