using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day10RegexMethods
{
    public class GenericsCustomization
    {
        public GenericsCustomization(){}
        public void ExampleOfList()
        {
            List<string> names = new List<string>();
        }
    }

    public class MyCollection : IList
    {
        public object? this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public int Add(object? value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object? value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object? value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object? value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object? value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }

    public static void Run()
    {
                // Regex matching
        string input = "Error: TIMEOUT while calling API.";
        string pattern = @"timeout";
        var rx = new Regex
        (
            pattern,
            RegexOptions.IgnoreCase,
            TimeSpan.FromMilliseconds(15)
        );
        Console.WriteLine(rx.IsMatch(input) ? "Found" : "Not Found");

        // Allocate memory
        var list = new List<byte[]>();
        for(int i =0; i<20000; i++)
        {
            list.Add(new byte[1024]); // many allocations
        }
        Console.WriteLine("Allocated");
        Console.WriteLine("Total memory: " + GC.GetTotalMemory(forceFullCollection: false));

        // Force garbage collection
        GC.Collect();

        // Create an instance of the GenericsCustomization class
        GenericsCustomization genericsCustomization = new GenericsCustomization();
        genericsCustomization.ExampleOfList();
    }
}