using System;

namespace Day20techstack
{
    public class Movie
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public int Ratings { get; set; }
    }

    public class MovieStock
    {
        public static List<Movie> MovieList = new List<Movie>();

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
            MovieList.Add(movie);
        }

        public static List<Movie> ViewMoviesByGenre(string genre)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in MovieList)
            {
                if (!string.IsNullOrEmpty(movie.Genre) && movie.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(movie);
                }
            }
            return result;
        }

        public static List<Movie> ViewMoviesByRatings()
        {
            return MovieList.OrderBy(movie => movie.Ratings).ToList();
        }

        public static void Run()
        {
            MovieList.Clear();

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

            Console.WriteLine("\nMovies Sorted by Ratings:");
            var sortedMovies = ViewMoviesByRatings();
            foreach (var movie in sortedMovies)
            {
                Console.WriteLine($"{movie.Title} -> {movie.Artist} -> {movie.Genre} -> {movie.Ratings}");
            }
        }
    }
}