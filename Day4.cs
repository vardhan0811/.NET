using System;

public class Day4
{
    #region Fields
    public int Id;
    public string? Name;
    public string? Description;
    public string? LogHistory;
    #endregion
    #region Constructor
    public Day4()
    {
    }

    public Day4(int id)
    {
        this.Id = id;
    }

    public Day4(int id, string? name) : this(id)
    {
        // Validate name does not contain "Idiot"
        if (name != null && name.ToLower().Contains("idiot"))
        {
            throw new ArgumentException("Name cannot contain the word 'Idiot'");
        }
        LogHistory += $"Object created with Id: {DateTime.Now.ToString()}";
        Console.WriteLine(LogHistory);
        this.Name = name;
    }


    public Day4(int id, string? name, string? description) : this(id, name)
    {
        // Validate name does not contain "Idiot"
        if (name != null && name.ToLower().Contains("idiot"))
        {
            throw new ArgumentException("Name cannot contain the word 'Idiot'");
        }
        LogHistory += $"Object created with Id and Name: {DateTime.Now.ToString()}";
        Console.WriteLine(LogHistory);
        this.Description = description;
    }

    public int AdditionResult;
    public Day4(int a, int b)
    {
        AdditionResult = a + b;
        Console.WriteLine($"Addition of {a} + {b} == {AdditionResult}");
    }
    #endregion
    #region MainMethod
    public static void Run(string[] args)
    {
        try
        {
            Day4 day4b = new Day4(2, "Sample Name");
            Console.WriteLine($"Day4b - Id: {day4b.Id}, Name: {day4b.Name}, Description: {day4b.Description}");

            // This will throw an exception
            Day4 day4c = new Day4(3, "Idiot Example", "This is a description.");
            Console.WriteLine($"Day4c - Id: {day4c.Id}, Name: {day4c.Name}, Description: {day4c.Description}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Example of Addition Constructor
        Console.WriteLine("--- Addition Constructor ---");
        Console.Write("Enter a: ");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter b: ");
        int b = Convert.ToInt32(Console.ReadLine());
        Day4 add = new Day4(a, b);

        add.ID = 100; // This will invoke the setter
        string? res = add.EmployeeDetails();
        Console.WriteLine(res);

        // Example of Associate class usage
        Associate associate = new Associate();
        associate.AssociateID = -1;
        associate.associateName = "";
        associate.associateRank = 50;
        Console.WriteLine(associate.AssociateError);

        // Example of Accounts and Savings class usage
        Savings savings = new Savings();
        savings.AccountId = 12345;
        savings.AccountName = "John's Savings";
        Console.WriteLine(savings.SavingsAccountInfo());
    }
    #endregion
    #region Methods
    // Property with validation
    private int id;
    public int ID
    {
        get { return id; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("ID must be positive.");
            }
            else
            {
                id = value;
            }
        }
    }
    public string? EmployeeDetails()
    {
        return $"Employee ID: {id}";
    }
    #endregion
    #region AssociateExample
    public class Associate
    {
        private int AssociateId;
        private string? AssociateName;
        private int Rank;
        public string? AssociateError;
        public int AssociateID
        {
            get { return AssociateId; }
            set
            {
                if (value <= 0)
                {
                    AssociateError += "Associate ID must be positive.";
                }
                else
                {
                    AssociateId = value;
                }
            }
        }
        public string? associateName 
        {
            get { return AssociateName; }
            set
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    AssociateError += "Associate Name cannot be empty.";
                }
                else
                {
                    AssociateName = value;
                }
            }
        }
        public int associateRank
        {
            get { return Rank; }
            set
            {
                if (value < 1 || value > 10)
                {
                    AssociateError += "Rank must be between 1 and 10.";
                }
                else
                {
                    Rank = value;
                }
            }
        }
    }
    #endregion
    #region AccountsExample
    public class Accounts
    {
        public int AccountId;
        public string? AccountName;
        public string GetAccountInfo()
        {
            return $"Account ID: {AccountId}, Account Name: {AccountName}";
        }
    }
    
    public class Savings : Accounts
    {
        public string SavingsAccountInfo()
        {
            string info = string.Empty;
            info += base.GetAccountInfo();
            info += ", Descendant from accounts - Account Type: Savings";
            return info;
        }
    }
    #endregion 
    #region FatherExample
    public class Father
    {
        // Virtual method to be overridden
        public virtual string InterestOn()
        {
            return "Interest on Finance";
        }
    }
    public class Son : Father
    {
        // Override InterestOn method
        public override string InterestOn()
        {
            return "Interest on Sports";
        }
    }
    #endregion
}
