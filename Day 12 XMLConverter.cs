using System.Xml; // Import System.Xml namespace
using System.IO; // Import System.IO namespace
using System.Xml.Serialization; // Import System.Xml.Serialization namespace

/// <summary>
/// Represents a student with a name, marks, and a list of relatives.
/// </summary>
public class Stu // Student class
{
    /// <summary>
    /// Gets or sets the name of the student.
    /// </summary>
    public string? Name { get; set; } // Student name property

    /// <summary>
    /// Gets or sets the marks obtained by the student.
    /// </summary>
    public int Marks { get; set; } // Student marks property

    /// <summary>
    /// Gets or sets the list of relatives of the student.
    /// </summary>
    public Person[]? Relatives { get; set; } // Array of relatives
}

/// <summary>
/// Represents a person with a name and a relation to the student.
/// </summary>
public class Person // Person class
{
    /// <summary>
    /// Gets or sets the name of the person.
    /// </summary>
    public string? Name { get; set; } // Person name property

    /// <summary>
    /// Gets or sets the relation of the person to the student.
    /// </summary>
    public string? Relation { get; set; } // Relation property
}

/// <summary>
/// Provides functionality to serialize a <see cref="Stu"/> object to XML and print the result to the console.
/// </summary>
public class Day12XMLConverter // Main class for Day 12
{
    /// <summary>
    /// Runs the XML and JSON serialization and deserialization examples.
    /// </summary>
    public static void Run() // Main method
    {
        // Create a new student
        Stu student = new Stu // Instantiate Stu object
        {
            // Set the student details
            Name = "Kishore", // Set name
            Marks = 95, // Set marks
            // Giving relations to the student
            Relatives = new Person[] // Set relatives array
            {
                new Person { Name = "Ravi", Relation = "Father" }, // Add father
                new Person { Name = "Sita", Relation = "Mother" } // Add mother
            }
        };

        Console.WriteLine("----------------XML format----------------"); // Print XML format header
        /// <summary>
        /// Serializes the student object to XML format.
        /// </summary>
        XmlSerializer serializer = new XmlSerializer(typeof(Stu)); // Create XML serializer
        // Serialize the student object to XML format
        using (StringWriter writer = new StringWriter()) // Create StringWriter
        {
            // Serialize the student object to XML format
            serializer.Serialize(writer, student); // Serialize to XML
            // Get the XML string
            string xml = writer.ToString(); // Get XML string
            // Print the XML string
            Console.WriteLine(xml); // Print XML
        }

        Console.WriteLine("----------------Serialization json format----------------"); // Print JSON format header
        /// <summary>
        /// Serializes the student object to JSON format.
        /// </summary>
        var jsonOptions = new System.Text.Json.JsonSerializerOptions // Create JSON options
        {
            // Configure JSON serialization options
            WriteIndented = true // Indent JSON
        };
        string json = System.Text.Json.JsonSerializer.Serialize(student, jsonOptions); // Serialize to JSON
        Console.WriteLine(json); // Print JSON

     
        Console.WriteLine("----------------Deserialization json format----------------"); // Print deserialization header
        /// <summary>
        /// Deserializes the student object to JSON format.
        /// </summary>
        var deserializedStudent = System.Text.Json.JsonSerializer.Deserialize<Stu>(json); // Deserialize JSON
        Console.WriteLine($"Name: {deserializedStudent?.Name}"); // Print name
        Console.WriteLine($"Marks: {deserializedStudent?.Marks}"); // Print marks
        Console.WriteLine("Relatives:"); // Print relatives label

        if (deserializedStudent?.Relatives != null) // If relatives exist
        {
            // Print the relatives of the student
            foreach (var relative in deserializedStudent.Relatives) // Loop through relatives
            {
                Console.WriteLine($" - {relative.Name} ({relative.Relation})"); // Print relative
            }
        }

        /// <summary>
        /// Example of using a delegate.
        /// </summary>
        DelegateExample.DelegateAddFunctionName addition = new DelegateExample.DelegateAddFunctionName(DelegateExample.Add); // Create delegate instance
    }

    // Delegation Example
    public class DelegateExample // Delegate example class
    {
        // Delegate declaration
        public delegate int DelegateAddFunctionName(int a, int b); // Delegate type
        // Method that matches the delegate signature
        public static int Add(int a, int b) // Add method
        {
            return a + b; // Return sum
        }
    }
}
