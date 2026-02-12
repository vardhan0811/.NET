using System;

namespace Day38
{
    class Employee
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public decimal Salary { get; private set; }

        // Constructor with validation
        public Employee(int id, string? name, string? email, decimal salary)
        {
            Id = id;
            Name = name;

            // Email validation
            if (email != null && email.Contains("@"))
                Email = email;
            else
                Email = "unknown@company.com";

            // Salary validation
            if (salary > 0)
                Salary = salary;
            else
                Salary = 30000;
        }

        public void Print()
        {
            Console.WriteLine(
                $"Id: {Id}, Name: {Name}, Email: {Email}, Salary: {Salary}");
        }
    }

    class EmployeeOnboarding
    {
        static void Run()
        {
            Employee e1 = new Employee(
                1,"Rahul","rahul@gmail.com",50000);

            Employee e2 = new Employee(
                2,"Anita","anitagmail.com",-2000);      

            Employee e3 = new Employee(3,"Kiran",null,0);       

            e1.Print();
            e2.Print();
            e3.Print();

            Console.ReadLine();
        }
    }
}