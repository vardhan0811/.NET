using System;
using Services;
using Domain;
using Exceptions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementService service = new ManagementService();

            while (true)
            {
                Console.WriteLine("\n1. Display Ranking");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Update GPA");
                Console.WriteLine("4. Remove Student");
                Console.WriteLine("5. Exit");

                Console.Write("Enter choice: ");
                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            foreach (Student s in service.GetAll())
                                Console.WriteLine($"{s.Id} {s.Name} GPA:{s.GPA}");
                            break;

                        case 2:
                            Student st = new Student();

                            Console.Write("Id: ");
                            st.Id = Console.ReadLine();

                            Console.Write("Name: ");
                            st.Name = Console.ReadLine();

                            Console.Write("GPA: ");
                            st.GPA = double.Parse(Console.ReadLine());

                            service.AddEntity(st.GPA, st);
                            break;

                        case 3:
                            Console.Write("Enter Id: ");
                            string uid = Console.ReadLine();

                            Console.Write("New GPA: ");
                            double newGpa = double.Parse(Console.ReadLine());

                            service.UpdateEntity(uid, newGpa);
                            break;

                        case 4:
                            Console.Write("Enter Id: ");
                            string rid = Console.ReadLine();

                            service.RemoveEntity(rid);
                            break;

                        case 5:
                            return;

                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch (CustomBaseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
