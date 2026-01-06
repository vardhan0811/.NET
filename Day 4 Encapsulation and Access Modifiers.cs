using System;

    /// <summary>
    /// Demonstrates encapsulation and access modifiers in C#.
    /// </summary>
    public class Day4Encapsulation
    {
        #region Fields
        /// <summary>
        /// The ID of the object.
        /// </summary>
        public int Id;
        /// <summary>
        /// The name of the object.
        /// </summary>
        public string? Name;
        /// <summary>
        /// The description of the object.
        /// </summary>
        public string? Description;
        /// <summary>
        /// The log history of the object.
        /// </summary>
        public string? LogHistory;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Day4Encapsulation()
        {
        }

        /// <summary>
        /// Constructor with ID.
        /// </summary>
        /// <param name="id">The ID to set.</param>
        public Day4Encapsulation(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Constructor with ID and name, validates name.
        /// </summary>
        /// <param name="id">The ID to set.</param>
        /// <param name="name">The name to set.</param>
        public Day4Encapsulation(int id, string? name) : this(id)
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

        /// <summary>
        /// Constructor with ID, name, and description, validates name.
        /// </summary>
        /// <param name="id">The ID to set.</param>
        /// <param name="name">The name to set.</param>
        /// <param name="description">The description to set.</param>
        public Day4Encapsulation(int id, string? name, string? description) : this(id, name)
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

        /// <summary>
        /// Stores the result of addition.
        /// </summary>
        public int AdditionResult;
        /// <summary>
        /// Constructor that adds two integers and prints the result.
        /// </summary>
        /// <param name="a">First integer.</param>
        /// <param name="b">Second integer.</param>
        public Day4Encapsulation(int a, int b)
        {
            AdditionResult = a + b;
            Console.WriteLine($"Addition of {a} + {b} == {AdditionResult}");
        }
        #endregion

        #region MainMethod
        /// <summary>
        /// Runs encapsulation and access modifier examples.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        public static void Run(string[] args)
        {
            try
            {
                // Create object with valid name
                Day4Encapsulation day4b = new Day4Encapsulation(2, "Sample Name");
                Console.WriteLine($"Day4b - Id: {day4b.Id}, Name: {day4b.Name}, Description: {day4b.Description}");
                // This will throw an exception
                Day4Encapsulation day4c = new Day4Encapsulation(3, "Idiot Example", "This is a description.");
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
            Day4Encapsulation add = new Day4Encapsulation(a, b);

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
        /// <summary>
        /// Private backing field for ID property.
        /// </summary>
        private int id;
        /// <summary>
        /// Property for ID with validation.
        /// </summary>
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
        /// <summary>
        /// Returns employee details as a string.
        /// </summary>
        /// <returns>Employee details string.</returns>
        public string? EmployeeDetails()
        {
            return $"Employee ID: {id}";
        }
        #endregion

        #region AssociateExample
        /// <summary>
        /// Demonstrates an associate with validation logic.
        /// </summary>
        public class Associate
        {
            private int AssociateId;
            private string? AssociateName;
            private int Rank;
            /// <summary>
            /// Stores error messages for validation.
            /// </summary>
            public string? AssociateError;
            /// <summary>
            /// Property for associate ID with validation.
            /// </summary>
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
            /// <summary>
            /// Property for associate name with validation.
            /// </summary>
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
            /// <summary>
            /// Property for associate rank with validation.
            /// </summary>
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
        /// <summary>
        /// Demonstrates an account with basic info.
        /// </summary>
        public class Accounts
        {
            /// <summary>
            /// The account ID.
            /// </summary>
            public int AccountId;
            /// <summary>
            /// The account name.
            /// </summary>
            public string? AccountName;
            /// <summary>
            /// Gets account info as a string.
            /// </summary>
            /// <returns>Account info string.</returns>
            public string GetAccountInfo()
            {
                return $"Account ID: {AccountId}, Account Name: {AccountName}";
            }
        }

        /// <summary>
        /// Demonstrates a savings account inheriting from Accounts.
        /// </summary>
        public class Savings : Accounts
        {
            /// <summary>
            /// Gets savings account info as a string.
            /// </summary>
            /// <returns>Savings account info string.</returns>
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
        /// <summary>
        /// Demonstrates a father class with a virtual method.
        /// </summary>
        public class Father
        {
            /// <summary>
            /// Virtual method to be overridden by descendants.
            /// </summary>
            /// <returns>Interest description.</returns>
            public virtual string InterestOn()
            {
                return "Interest on Finance";
            }
        }
        /// <summary>
        /// Demonstrates a son class overriding the father's method.
        /// </summary>
        public class Son : Father
        {
            /// <summary>
            /// Overrides InterestOn method.
            /// </summary>
            /// <returns>Interest description.</returns>
            public override string InterestOn()
            {
                return "Interest on Sports";
            }
        }
        #endregion
    }
