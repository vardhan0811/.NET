#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Day23_CSharp8_Features
{
    /// <summary>
    /// Entry point of the application.
    /// Demonstrates multiple C# 8.0 features in a single, realistic mini project.
    /// </summary>
    internal class Day23_CSharp8_Features
    {
        /// <summary>
        /// Application start.
        /// Uses async Main (C# 7.1+) to support async streams.
        /// </summary>
        static async Task Run(string[] args)
        {
            // Nullable reference type usage:
            // args may be empty, so we safely handle it
            string? inputPath = args.Length > 0 ? args[0] : null;

            await LogProcessor.RunAsync(inputPath);

            Console.WriteLine("Log processing completed.");
        }
    }

    /// <summary>
    /// Processes log files using modern C# 8.0 features:
    /// - Nullable reference types
    /// - Async streams (IAsyncEnumerable)
    /// - Using declarations
    /// - Switch expressions with pattern matching
    /// - Null-coalescing assignment
    /// </summary>
    public static class LogProcessor
    {
        /// <summary>
        /// Runs the log processing workflow.
        /// </summary>
        /// <param name="inputPath">Path to the input log file (nullable).</param>
        public static async Task RunAsync(string? inputPath)
        {
            // Nullable reference types: explicit validation
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                Console.WriteLine("❌ Input file path is missing.");
                return;
            }

            // Null-coalescing assignment (??=)
            // Initializes the summary list only when needed
            List<string>? summary = null;
            summary ??= new List<string>();

            // Using declaration (C# 8.0)
            // Resource is disposed automatically at the end of scope
            using var writer = new StreamWriter("summary.txt");

            // Asynchronous stream consumption (await foreach)
            await foreach (var line in ReadLogLinesAsync(inputPath))
            {
                var category = ClassifyLog(line);

                summary.Add(category);
                await writer.WriteLineAsync($"{category}: {line}");
            }

            Console.WriteLine($"Processed {summary.Count} log entries.");
        }

        /// <summary>
        /// Reads log lines asynchronously using async streams.
        /// </summary>
        /// <param name="path">File path (assumed non-null here).</param>
        /// <returns>Asynchronous stream of log lines.</returns>
        public static async IAsyncEnumerable<string> ReadLogLinesAsync(string path)
        {
            // Using declaration instead of using { }
            using var reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                // Pattern matching with null check
                if (line is not null)
                {
                    yield return line;
                }
            }
        }

        /// <summary>
        /// Classifies a log line using C# 8.0 switch expressions
        /// and pattern matching.
        /// </summary>
        /// <param name="line">Log line content.</param>
        /// <returns>Log category.</returns>
        private static string ClassifyLog(string line) => line switch
        {
            var l when l.Contains("ERROR", StringComparison.OrdinalIgnoreCase)   => "ERROR",
            var l when l.Contains("WARN", StringComparison.OrdinalIgnoreCase)    => "WARNING",
            var l when l.Contains("INFO", StringComparison.OrdinalIgnoreCase)    => "INFO",
            _                                                                    => "GENERAL"
        };
    }
}
