using Exceptions;

namespace Domain
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public double GPA { get; set; }

        // Validate business rules
        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Student Id Required");

            if (string.IsNullOrWhiteSpace(Name))
                throw new ScenarioException("Student Name Required");

            if (GPA < 0 || GPA > 10)
                throw new ScenarioException("Invalid GPA (Must be 0–10)");
        }
    }
}
