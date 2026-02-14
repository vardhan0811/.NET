using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = RiskRating
        private SortedDictionary<int, List<Investment>> _data =
            new SortedDictionary<int, List<Investment>>();

        public void AddInvestment(Investment inv)
        {
            inv.Validate();

            // Duplicate Check
            foreach (var list in _data.Values)
            {
                if (list.Exists(i => i.Id == inv.Id))
                    throw new ScenarioException("Duplicate Investment");
            }

            if (!_data.ContainsKey(inv.RiskRating))
                _data[inv.RiskRating] = new List<Investment>();

            _data[inv.RiskRating].Add(inv);
        }

        public IEnumerable<Investment> GetAll()
        {
            foreach (var pair in _data)
                foreach (var inv in pair.Value)
                    yield return inv;
        }

        public void UpdateRisk(string id, int newRisk)
        {
            if (newRisk < 1 || newRisk > 5)
                throw new ScenarioException("Invalid Risk Rating");

            foreach (var pair in _data.ToList())
            {
                var inv = pair.Value.Find(i => i.Id == id);
                if (inv != null)
                {
                    pair.Value.Remove(inv);

                    inv.RiskRating = newRisk;

                    if (!_data.ContainsKey(newRisk))
                        _data[newRisk] = new List<Investment>();

                    _data[newRisk].Add(inv);
                    return;
                }
            }

            throw new ScenarioException("Investment Not Found");
        }
    }
}
