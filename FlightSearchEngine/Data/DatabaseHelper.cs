using FlightSearchEngine.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FlightSearchEngine.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 1️⃣ Get Sources
        public async Task<List<string>> GetSourcesAsync()
        {
            var sources = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetSources", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sources.Add(reader.GetString(0));
                    }
                }
            }

            return sources;
        }

        // 2️⃣ Get Destinations
        public async Task<List<string>> GetDestinationsAsync()
        {
            var destinations = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetDestinations", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        destinations.Add(reader.GetString(0));
                    }
                }
            }

            return destinations;
        }

        // 3️⃣ Search Flights Only
        public async Task<List<FlightResult>> SearchFlightsAsync(string source, string destination, int persons)
        {
            var flights = new List<FlightResult>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_SearchFlights", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@Persons", persons);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        flights.Add(new FlightResult
                        {
                            FlightId = reader.GetInt32(0),
                            FlightName = reader.GetString(1),
                            FlightType = reader.GetString(2),
                            Source = reader.GetString(3),
                            Destination = reader.GetString(4),
                            TotalCost = reader.GetDecimal(5)
                        });
                    }
                }
            }

            return flights;
        }

        // 4️⃣ Search Flights + Hotels
        public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source, string destination, int persons)
        {
            var packages = new List<FlightHotelResult>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_SearchFlightsWithHotels", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@Persons", persons);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        packages.Add(new FlightHotelResult
                        {
                            FlightId = reader.GetInt32(0),
                            FlightName = reader.GetString(1),
                            Source = reader.GetString(2),
                            Destination = reader.GetString(3),
                            HotelName = reader.GetString(4),
                            TotalCost = reader.GetDecimal(5)
                        });
                    }
                }
            }

            return packages;
        }
    }
}