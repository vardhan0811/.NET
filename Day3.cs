using System;
public class Day3
{
    public class Person
    {
        public string? Name;
        public int? Age;
    }
    
    // Inherit Person class
    public class Man : Person 
    {
        public string? Playing;
    }

    public class Woman : Person 
    {
        public string? Cooking;
    }

    public class Child : Person 
    {
        public string? watchingCartoon;
    }

    public static void Run(string[] args)
    {
        Person person = new Person()
        {
            Name = "Alex",
            Age = 30
        };

        Man man = new Man()
        {
            Name = "John",
            Age = 35,
            Playing = "Football"
        };

        Woman woman = new Woman()
        {
            Name = "Emily",
            Age = 30,
            Cooking = "Italian"
        };

        Person personWoman = woman;

        Child child = new Child()
        {
            Name = "Tommy",
            Age = 5,
            watchingCartoon = "Paw Patrol"
        };
        GetDetails(person);
        GetDetails(child);

    }

    // Get manDetails method
    public static string GetManDetails(Man input)
    {
        return $"Name: {input.Name}, Age: {input.Age}, Playing: {input.Playing}";
    }

    // Get womanDetails method
    public static string GetWomanDetails(Woman input)
    {
        return $"Name: {input.Name}, Age: {input.Age}, Cooking: {input.Cooking}";
    }

    // GetDetails method
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