using System;

/// <summary>
/// Demonstrates classes, objects, and inheritance concepts in C#.
/// </summary>
public class Day03ClassesObjectsInheritance
{
    /// <summary>
    /// Represents a generic person with a name and age.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string? Name;
        /// <summary>
        /// Gets or sets the age of the person.
        /// </summary>
        public int? Age;
    }

    /// <summary>
    /// Represents a man, inheriting from Person, with a playing activity.
    /// </summary>
    public class Man : Person
    {
        /// <summary>
        /// Gets or sets the activity the man is playing.
        /// </summary>
        public string? Playing;
    }

    /// <summary>
    /// Represents a woman, inheriting from Person, with a cooking activity.
    /// </summary>
    public class Woman : Person
    {
        /// <summary>
        /// Gets or sets the type of cooking the woman does.
        /// </summary>
        public string? Cooking;
    }

    /// <summary>
    /// Represents a child, inheriting from Person, with a favorite cartoon.
    /// </summary>
    public class Child : Person
    {
        /// <summary>
        /// Gets or sets the cartoon the child is watching.
        /// </summary>
        public string? watchingCartoon;
    }

    /// <summary>
    /// Runs the demonstration of object creation and inheritance.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    public static void Run(string[] args)
    {
        // Create a generic person
        Person person = new Person()
        {
            Name = "Alex",
            Age = 30
        };

        // Create a man object
        Man man = new Man()
        {
            Name = "John",
            Age = 35,
            Playing = "Football"
        };

        // Create a woman object
        Woman woman = new Woman()
        {
            Name = "Emily",
            Age = 30,
            Cooking = "Italian"
        };

        // Assign woman to a person reference
        Person personWoman = woman;

        // Create a child object
        Child child = new Child()
        {
            Name = "Tommy",
            Age = 5,
            watchingCartoon = "Paw Patrol"
        };
        // Print details of person and child
        GetDetails(person);
        GetDetails(child);
    }

    /// <summary>
    /// Gets the details of a man.
    /// </summary>
    /// <param name="input">The man object.</param>
    /// <returns>Details string.</returns>
    public static string GetManDetails(Man input)
    {
        return $"Name: {input.Name}, Age: {input.Age}, Playing: {input.Playing}";
    }

    /// <summary>
    /// Gets the details of a woman.
    /// </summary>
    /// <param name="input">The woman object.</param>
    /// <returns>Details string.</returns>
    public static string GetWomanDetails(Woman input)
    {
        return $"Name: {input.Name}, Age: {input.Age}, Cooking: {input.Cooking}";
    }

    /// <summary>
    /// Gets the details of a person, determining type at runtime.
    /// </summary>
    /// <param name="person">The person object.</param>
    /// <returns>Details string.</returns>
    public static string GetDetails(Person person)
    {
        if (person is Man man)
        {
            return GetManDetails(man);
        }
        else if (person is Woman woman)
        {
            return GetWomanDetails(woman);
        }
        return "Unknown Person";
    }
}