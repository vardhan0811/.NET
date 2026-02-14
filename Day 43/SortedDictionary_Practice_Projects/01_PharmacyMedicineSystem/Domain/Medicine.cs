using Exceptions;

namespace Domain
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int ExpiryYear { get; set; }

        public Medicine(string id, string name, int price, int expiryYear)
        {
            Id = id;
            Name = name;
            Price = price;
            ExpiryYear = expiryYear;

            Validate();
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Invalid Medicine Id");

            if (Price <= 0)
                throw new ScenarioException("Invalid Price");

            if (ExpiryYear < DateTime.Now.Year)
                throw new ScenarioException("Invalid Expiry Year");
        }

        public override string ToString()
        {
            return $"Details: {Id} {Name} {Price} {ExpiryYear}";
        }
    }
}
