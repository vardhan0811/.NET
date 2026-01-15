using System;

namespace Day20techstack
{
    /// <summary>
    /// Represents a movie with title, artist, genre, and ratings.
    /// </summary>
    public class Movie
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public int Ratings { get; set; }
    }

    /// <summary>
    /// Provides methods to manage and query a collection of movies.
    /// </summary>
    public class MovieStock
    {
        /// <summary>
        /// Stores the list of movies.
        /// </summary>
        public static List<Movie> MovieList = new List<Movie>();

        /// <summary>
        /// Adds a movie to the MovieList from a comma-separated string.
        /// </summary>
        /// <param name="MovieDetails">Comma-separated movie details (Title,Artist,Genre,Ratings)</param>
        public static void AddMovie(string MovieDetails)
        {
            string[] parts = MovieDetails.Split(',');
            Movie movie = new Movie
            {
                Title = parts[0].Trim(),
                Artist = parts[1].Trim(),
                Genre = parts[2].Trim(),
                Ratings = int.Parse(parts[3].Trim())
            };
            MovieList.Add(movie); // Add the movie to the list
        }

        /// <summary>
        /// Returns a list of movies filtered by genre.
        /// </summary>
        /// <param name="genre">Genre to filter by</param>
        /// <returns>List of movies in the specified genre</returns>
        public static List<Movie> ViewMoviesByGenre(string genre)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in MovieList)
            {
                // Check if the movie's genre matches the filter
                if (!string.IsNullOrEmpty(movie.Genre) && movie.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(movie);
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a list of movies sorted by their ratings in ascending order.
        /// </summary>
        /// <returns>List of movies sorted by ratings</returns>
        public static List<Movie> ViewMoviesByRatings()
        {
            return MovieList.OrderBy(movie => movie.Ratings).ToList();
        }

        /// <summary>
        /// Runs the movie management process by taking user input and displaying results.
        /// </summary>
        public static void Run()
        {
            MovieList.Clear(); // Clear the list before starting

            Console.WriteLine("Enter number of movies to add:");
            string? inputStr = Console.ReadLine();
            int input = 0;
            if (!string.IsNullOrEmpty(inputStr))
            {
                input = int.Parse(inputStr);
            }

            Console.WriteLine("Enter movie details (Title,Artist,Genre,Ratings):");
            for (int i = 0; i < input; i++)
            {
                Console.WriteLine($"Movie {i + 1}:");
                AddMovie(Console.ReadLine() ?? "");
            }

            // Filter movies by genre
            Console.WriteLine("Enter genre to filter:");
            string genre = Console.ReadLine() ?? "";
            var moviesByGenre = ViewMoviesByGenre(genre);
            if (moviesByGenre.Count == 0)
            {
                Console.WriteLine($"No Movies Found in genre {genre}");
            }
            else
            {
                Console.WriteLine($"Movies Found in genre {genre}:");
                foreach (var movie in moviesByGenre)
                {
                    Console.WriteLine($"{movie.Title} -> {movie.Artist} -> {movie.Genre} -> {movie.Ratings}");
                }
            }

            // Display movies sorted by ratings
            Console.WriteLine("\nMovies Sorted by Ratings:");
            var sortedMovies = ViewMoviesByRatings();
            foreach (var movie in sortedMovies)
            {
                Console.WriteLine($"{movie.Title} -> {movie.Artist} -> {movie.Genre} -> {movie.Ratings}");
            }
        }
    }
}