using System;

namespace Day21Important
{
    /// <summary>
    /// Interface representing a film with title, director, and release year.
    /// </summary>
    public interface IFilm
    {
        string Title { get; set; }
        string Director { get; set; }
        int ReleaseYear { get; set; }
    }
    
    /// <summary>
    /// Interface for film library operations.
    /// </summary>
    public interface IFilmLibrary
    {
        void AddFilm(IFilm file);
        void RemoveFile(string title);
        List<IFilm> GetFilms();
        List<IFilm> SearchFilms(string query);
        int GetTotalFilmCount();
    }

    /// <summary>
    /// Represents a film with properties for library management.
    /// </summary>
    public class Film : IFilm
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }

        public Film(string title, string director, int releaseYear)
        {
            Title = title;
            Director = director;
            ReleaseYear = releaseYear;
        }
    }

    /// <summary>
    /// Implements the film library for managing a collection of films.
    /// </summary>
    public class FilmLibrary : IFilmLibrary
    {
        private List<IFilm> _films = new List<IFilm>();

        /// <summary>
        /// Adds a film to the library.
        /// </summary>
        public void AddFilm(IFilm film)
        {
            _films.Add(film);
        }

        /// <summary>
        /// Removes all films with the specified title from the library.
        /// </summary>
        public void RemoveFile(string title)
        {
            _films.RemoveAll(f => f.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns a list of all films in the library.
        /// </summary>
        public List<IFilm> GetFilms()
        {
            return new List<IFilm>(_films);
        }

        /// <summary>
        /// Searches for films by title or director containing the query string.
        /// </summary>
        public List<IFilm> SearchFilms(string query)
        {
            return _films.Where(f => f.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                    f.Director.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Returns the total number of films in the library.
        /// </summary>
        public int GetTotalFilmCount()
        {
            return _films.Count;
        }
    }

    /// <summary>
    /// Entry point for the Movie Library system.
    /// </summary>
    public class MovieLibrary
    {
        /// <summary>
        /// Runs the movie library demo by adding films and displaying results.
        /// </summary>
        public static void Run()
        {
            IFilmLibrary filmLibrary = new FilmLibrary();

            // Add sample films to the library
            filmLibrary.AddFilm(new Film("Inception", "Christopher Nolan", 2010));
            filmLibrary.AddFilm(new Film("The Matrix", "The Wachowskis", 1999));
            filmLibrary.AddFilm(new Film("Interstellar", "Christopher Nolan", 2014));

            Console.WriteLine($"Total Films: {filmLibrary.GetTotalFilmCount()}");

            // Search for films by director
            var nolanFilms = filmLibrary.SearchFilms("Nolan");
            Console.WriteLine($"Films by Nolan: {nolanFilms.Count}");   
        }
    }
}