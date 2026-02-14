using Exceptions;

namespace Domain
{
    public class Account : BaseEntity
    {
        public string HolderName { get; set; }
        public decimal Balance { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Account Number Required");

            if (string.IsNullOrWhiteSpace(HolderName))
                throw new ScenarioException("Holder Name Required");

            if (Balance < 0)
                throw new ScenarioException("Negative Balance Not Allowed");
        }
    }
}
