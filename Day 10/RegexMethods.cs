using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Demonstrates regex, generics, collections, and memory management in C#.
/// </summary>
public class Day10RegexMethods
{
    /// <summary>
    /// Demonstrates customization of generics with a list example.
    /// </summary>
    public class GenericsCustomization
    {
        /// <summary>
        /// Initializes a new instance of GenericsCustomization.
        /// </summary>
        public GenericsCustomization(){}
        /// <summary>
        /// Example method showing usage of a generic list.
        /// </summary>
        public void ExampleOfList()
        {
            List<string> names = new List<string>();
        }
    }

    /// <summary>
    /// Custom collection implementing IList interface.
    /// </summary>
    public class MyCollection : IList
    {
        // Indexer to get or set an item at a specific index
        public object? this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // Indicates whether the collection has a fixed size
        public bool IsFixedSize => throw new NotImplementedException();

        // Indicates whether the collection is read-only
        public bool IsReadOnly => throw new NotImplementedException();

        // Gets the number of elements in the collection
        public int Count => throw new NotImplementedException();

        // Indicates whether access to the collection is synchronized (thread safe)
        public bool IsSynchronized => throw new NotImplementedException();

        // Gets an object that can be used to synchronize access to the collection
        public object SyncRoot => throw new NotImplementedException();

        // Adds an item to the collection
        public int Add(object? value)
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Removes all items from the collection
        public void Clear()
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Determines whether the collection contains a specific value
        public bool Contains(object? value)
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Copies the elements of the collection to an Array, starting at a particular Array index
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Returns an enumerator that iterates through the collection
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Determines the index of a specific item in the collection
        public int IndexOf(object? value)
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Inserts an item to the collection at the specified index
        public void Insert(int index, object? value)
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Removes the first occurrence of a specific object from the collection
        public void Remove(object? value)
        {
            throw new NotImplementedException(); // Not implemented
        }

        // Removes the item at the specified index
        public void RemoveAt(int index)
        {
            throw new NotImplementedException(); // Not implemented
        }
    }

    /// <summary>
    /// Runs regex, memory, and generics demonstrations.
    /// </summary>
    public static void Run()
    {
        // Declare a string variable for input text
        string input = "Error: TIMEOUT while calling API."; // Input string to search
        // Declare a string variable for the regex pattern
        string pattern = @"timeout"; // Pattern to match (case-insensitive)
        // Create a Regex object with pattern, options, and timeout
        var rx = new Regex( // Regex object for matching
            pattern, // Pattern to use
            RegexOptions.IgnoreCase, // Ignore case in matching
            TimeSpan.FromMilliseconds(15) // Set a timeout for regex
        );
        // Check if the pattern matches the input and print result
        Console.WriteLine(rx.IsMatch(input) ? "Found" : "Not Found"); // Output match result

        // Allocate memory for demonstration
        var list = new List<byte[]>(); // List to hold byte arrays
        for(int i =0; i<20000; i++) // Loop 20,000 times
        {
            list.Add(new byte[1024]); // Allocate 1KB and add to list
        }
        // Print allocation status
        Console.WriteLine("Allocated"); // Indicate allocation done
        // Print total memory used
        Console.WriteLine("Total memory: " + GC.GetTotalMemory(forceFullCollection: false)); // Show memory usage

        // Force garbage collection
        GC.Collect(); // Trigger garbage collection

        // Create an instance of the GenericsCustomization class
        GenericsCustomization genericsCustomization = new GenericsCustomization(); // Instantiate GenericsCustomization
        genericsCustomization.ExampleOfList(); // Call example method
    }
}