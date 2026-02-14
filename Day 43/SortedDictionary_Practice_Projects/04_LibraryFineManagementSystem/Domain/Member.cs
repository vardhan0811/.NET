using Exceptions;

namespace Domain
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public int FineAmount { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Member Id Required");

            if (string.IsNullOrWhiteSpace(Name))
                throw new ScenarioException("Member Name Required");

            if (FineAmount < 0)
                throw new ScenarioException("Invalid Fine Amount");
        }
    }
}
