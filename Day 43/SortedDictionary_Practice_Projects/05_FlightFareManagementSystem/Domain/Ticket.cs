using Exceptions;

namespace Domain
{
    public class Ticket : BaseEntity
    {
        public string PassengerName { get; set; }
        public int Fare { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Ticket Id Required");

            if (string.IsNullOrWhiteSpace(PassengerName))
                throw new ScenarioException("Passenger Name Required");

            if (Fare <= 0)
                throw new ScenarioException("Invalid Fare");
        }
    }
}
