using Exceptions;

namespace Domain
{
    public class SupportTicket : BaseEntity
    {
        public string IssueDescription { get; set; }
        public int SeverityLevel { get; set; }  // 1 to 5

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Ticket Id Required");

            if (string.IsNullOrWhiteSpace(IssueDescription))
                throw new ScenarioException("Issue Description Required");

            if (SeverityLevel < 1 || SeverityLevel > 5)
                throw new ScenarioException("Invalid Severity Level");
        }
    }
}
