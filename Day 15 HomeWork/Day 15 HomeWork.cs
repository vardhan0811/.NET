using System;
using System.Collections.Generic;

namespace Day15HomeWork
{
    /* 
    1. Implement a C# program based on the following specifications:
    Class: Person
        Properties (Auto-implemented, public):
        ▪ Name : string
        ▪ Address : string
        ▪ Age : int
    */
    public class Person
    {
        public string? Name { get; set; }
        public string? Address {get; set; }
        public int Age { get; set; }
    }

    /* Class: PersonImplementation
    GetName(IList<Person> person)
    ▪ Return type: string
    ▪ Visibility: public
    ▪ Display the name and address of all persons in the list. */
    public class PersonImplementation
    {
        public string GetName(IList<Person> person)
        {
            foreach (Person p in person)
            {
                Console.WriteLine($"Name: {p.Name}, Address: {p.Address}");
            }
            return "Displayed all person details";
        }

        /*o Average(IList<Person> person)
        ▪ Return type: double
        ▪ Visibility: public
        */
        public double Average(IList<Person> person)
        {
            int totalAge = 0;
            foreach(Person p in person)
            {
                totalAge += p.Age;
            }
            return (double)totalAge / person.Count;
        }

        /*o Max(IList<Person> person)
        ▪ Return type: int
        ▪ Visibility: public
        ▪ Find the maximum age among all persons.
        */
        public int Max(IList<Person> person)
        {
            int maxAge = person[0].Age;
            foreach (Person p in person)
            {
                if (p.Age > maxAge)
                {
                    maxAge = p.Age;
                }
            }
            return maxAge;
        }
    }

    /*Question 2: Method Overloading
    2. Implement a C# program demonstrating method overloading as per specifications:
        Class: Source
        o Add(int a, int b, int c)
        ▪ Return type: int
        ▪ Visibility: public
        ▪ Add three integer values.
    */
    public class Source
    {
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }

    /*
    o Add(double a, double b, double c)
    ▪ Return type: double
    ▪ Visibility: public
    ▪ Add three double values.
    */
        public double Add(double a, double b, double c)
        {
            return a + b + c;
        
        }
    }

    /*3. Implement a C# program to calculate bill amount with tax based on specifications:
    Enum: CommodityCategory
    o Furniture
    o Grocery
    o Service
    */
    public enum CommodityCategory
    {
        Furniture,
        Grocery,
        Service
    }

    /*Class: Commodity
    o Constructor:
    ▪ Commodity(CommodityCategory category, string commodityName,
    int commodityQuantity, double commodityPrice)
    o Properties (public):
    ▪ Category : CommodityCategory
    ▪ CommodityName : string
    ▪ CommodityQuantity : int
    ▪ CommodityPrice : double
    */
    public class Commodity
    {
        public CommodityCategory Category { get; set; }
        public string CommodityName { get; set; }
        public int CommodityQuantity { get; set; }
        public double CommodityPrice { get; set; }

        public Commodity(CommodityCategory category, string commodityName, int commodityQuantity, double commodityPrice)
        {
            Category = category;
            CommodityName = commodityName;
            CommodityQuantity = commodityQuantity;
            CommodityPrice = commodityPrice;
        }
    }

    // Class: PrepareBill
    public class PrepareBill
    {
        /*   
        o Member variable:
        ▪ _taxRates : IDictionary<CommodityCategory, double> (readonly, private)
        */
        private readonly IDictionary<CommodityCategory, double> _taxRates;

        /*
        o Constructor:
        ▪ Initialize _taxRates with an empty dictionary.
        */
        public PrepareBill()
        {
            _taxRates = new Dictionary<CommodityCategory, double>();
        }

        /*    
        o Methods:
            ▪ SetTaxRates(CommodityCategory category, double taxRate)
            ▪ Return type: void
            ▪ Visibility: public
            # Add tax rate for a category if not already present.
        */
        public void SetTaxRates(CommodityCategory category, double taxRate)
        {
            if (!_taxRates.ContainsKey(category))
            {
                _taxRates[category] = taxRate;
            }
        }

        /*
        ▪ CalculateBillAmount(IList<Commodity> items)
        ▪ Return type: double
        ▪ Visibility: public
        # Calculate total bill amount.
        -> Throw ArgumentException if tax rate for a category is not set
        */
        public double CalculateBillAmount(IList<Commodity> items)
        {
            double totalAmount = 0.0;

            foreach (var item in items) 
            {
                if (!_taxRates.ContainsKey(item.Category)) // Check if tax rate is set
                {
                    throw new ArgumentException($"Tax rate for category {item.Category} is not set.");
                }

                double taxRate = _taxRates[item.Category];
                double itemTotal = item.CommodityPrice * item.CommodityQuantity;
                double taxAmount = itemTotal * taxRate / 100;
                totalAmount += itemTotal + taxAmount;
            }

            return totalAmount;
        }
    }

    /*
    4. Implement a C# program for broadband subscription calculation:
    Interface: IBroadbandPlan
    o GetBroadbandPlanAmount()
    ▪ Return type: int
    */
    public interface IBroadbandPlan
    {
        int GetBroadbandPlanAmount();
    }

    /*Class: Black : IBroadbandPlan
    o Fields:
        ▪ _isSubscriptionValid : bool (private, readonly)
        ▪ _discountPercentage : int (private, readonly)
    o Constant:
        ▪ PlanAmount = 3000
    o Constructor:
        ▪ Black(bool isSubscriptionValid, int discountPercentage)
        ▪ Throw ArgumentOutOfRangeException if discountPercentage < 0 or > 50
    o Method:
        ▪ GetBroadbandPlanAmount()
        ▪ Return discounted amount if subscription is valid, else return normal price.
    */
    public class Black : IBroadbandPlan
    {
        private readonly bool _isSubscriptionValid; // Indicates if the subscription is valid
        private readonly int _discountPercentage; // Discount percentage for the plan
        private const int PlanAmount = 3000; // Base plan amount

        public Black(bool isSubscriptionValid, int discountPercentage) 
        {
            if (discountPercentage < 0 || discountPercentage > 50) // Validate discount percentage
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 50."); // Validate discount percentage
            }

            _isSubscriptionValid = isSubscriptionValid; // Set subscription validity
            _discountPercentage = discountPercentage; // Set discount percentage
        }

        public int GetBroadbandPlanAmount() // Get broadband plan amount
        {
            if (_isSubscriptionValid) // Check if subscription is valid
            {
                double discountAmount = PlanAmount * _discountPercentage / 100.0; // Calculate discount amount
                return (int)(PlanAmount - discountAmount);
            }
            else
            {
                return PlanAmount;
            }
        }
    }

    /*Class: SubscribePlan
    o Field:
        ▪ _broadbandPlans : IList<IBroadbandPlan> (private, readonly)
    o Constructor:
        ▪ SubscribePlan(IList<IBroadbandPlan> broadbandPlans)
        ▪ Throw ArgumentNullException if input list is null
    o Method:
        ▪ GetSubscriptionPlan()
        ▪ Return type: IList<Tuple<string, int>>
    */
    public class SubscribePlan
    {
        private readonly IList<IBroadbandPlan> _broadbandPlans;

        public SubscribePlan(IList<IBroadbandPlan> broadbandPlans)
        {
            _broadbandPlans = broadbandPlans ?? throw new ArgumentNullException(nameof(broadbandPlans), "Broadband plans list cannot be null.");
        }

        public IList<Tuple<string, int>> GetSubscriptionPlan()
        {
            var subscriptionPlans = new List<Tuple<string, int>>();

            foreach (var plan in _broadbandPlans)
            {
                subscriptionPlans.Add(new Tuple<string, int>(plan.GetType().Name, plan.GetBroadbandPlanAmount()));
            }

            return subscriptionPlans;
        }
    }

    /*
    5. Implement a C# program to determine scholarship eligibility: 
    Delegate
    o IsEligibleForScholarship(Student std)
        ▪ Return type: bool
        ▪ Visibility: public
    */
    public delegate bool IsEligibleForScholarship(Student std);

    /*
    Class: Student
    o Properties (Auto-implemented, public):
        ▪ RollNo : int
        ▪ Name : string
        ▪ Marks : int
        ▪ SportsGrade : char
    */
    public class Student
    {
        public int RollNo { get; set; }
        public string? Name { get; set; }
        public int Marks { get; set; }
        public char SportsGrade { get; set; }
    }

    /*
    Class: Programs
    o Method:
        ▪ ScholarshipEligibility(Student std)
        ▪ Return type: bool
        ▪ Visibility: public static
    */
    public class Programs
    {
        public static bool ScholarshipEligibility(Student std)
        {
            if (std.Marks > 80 || std.SportsGrade == 'A')
            {
                return true;
            }
            return false;
        }

    /*
    Method
    o GetEligibleStudents(List<Student> studentsList, IsEligibleForScholarship isEligible)
        ▪ Return type: string
        ▪ Visibility: public static
        ▪ Return comma-separated eligible student names. 
    Eligibility Criteria:
        o Marks > 80
        o SportsGrade = 'A'
    */

        public static string GetEligibleStudents(List<Student> studentsList, IsEligibleForScholarship isEligible)
        {
            List<string> eligibleStudentNames = new List<string>();
            foreach (var student in studentsList)
            {
                if (isEligible(student))
                {
                    eligibleStudentNames.Add(student.Name ?? string.Empty);
                }
            }
            return string.Join(", ", eligibleStudentNames);
        }
    }
}

