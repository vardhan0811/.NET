using Exceptions;

namespace Domain
{
    public class Investment : BaseEntity
    {
        public string AssetName { get; set; }
        public int RiskRating { get; set; }   // 1 to 5

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new ScenarioException("Investment Id Required");

            if (string.IsNullOrWhiteSpace(AssetName))
                throw new ScenarioException("Asset Name Required");

            if (RiskRating < 1 || RiskRating > 5)
                throw new ScenarioException("Invalid Risk Rating");
        }
    }
}
