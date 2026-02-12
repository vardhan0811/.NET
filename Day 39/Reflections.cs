using System;
using System.Reflection;

namespace Day39
{
    // ===================== TARGET CLASS =====================
    public class Person
    {
        public string? Name { get; set; }

        private string? secretCode = "INIT123";

        public Person(string? name)
        {
            Name = name;
        }

        public void Show()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"SecretCode: {secretCode}");
        }
    }

    public class Reflections
    {
        public static void Run()
        {
            // Step 1: Create normal object
            Person p = new Person("Elijah");

            Console.WriteLine("=== BEFORE REFLECTION ===");
            p.Show();

            // Step 2: Get Type metadata
            Type type = typeof(Person);

            // Step 3: Get PRIVATE field using BindingFlags
            FieldInfo? secretField =
                type.GetField(
                    "secretCode",
                    BindingFlags.Instance | BindingFlags.NonPublic);

            // Step 4: READ private field value
            string? oldValue =
                (string?)secretField?.GetValue(p);

            Console.WriteLine();
            Console.WriteLine("Private field read via reflection: " + oldValue);

            // Step 5: MODIFY private field value
            secretField?.SetValue(p, "HACKED999");

            Console.WriteLine();
            Console.WriteLine("=== AFTER REFLECTION ===");
            p.Show();

            Console.ReadLine();
        }
    }
}
