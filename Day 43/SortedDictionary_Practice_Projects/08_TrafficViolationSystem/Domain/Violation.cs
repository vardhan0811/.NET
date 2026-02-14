using Exceptions;

namespace Domain
{
    public class Violation : BaseEntity
    {
        public string OwnerName { get; set; }
        public int FineAmount { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Vehicle Number Required");

            if (string.IsNullOrWhiteSpace(OwnerName))
                throw new ScenarioException("Owner Name Required");

            if (FineAmount <= 0)
                throw new ScenarioException("Invalid Fine Amount");
        }
    }
}
