using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = FineAmount
        private SortedDictionary<int, List<Violation>> _data =
            new SortedDictionary<int, List<Violation>>();

        public void AddViolation(Violation v)
        {
            v.Validate();

            // Duplicate check (Vehicle already recorded)
            foreach (var list in _data.Values)
            {
                if (list.Exists(x => x.Id == v.Id))
                    throw new ScenarioException("Duplicate Violation");
            }

            if (!_data.ContainsKey(v.FineAmount))
                _data[v.FineAmount] = new List<Violation>();

            _data[v.FineAmount].Add(v);
        }

        public IEnumerable<Violation> GetAll()
        {
            // Show descending fine
            foreach (var pair in _data.Reverse())
                foreach (var v in pair.Value)
                    yield return v;
        }

        public void PayFine(string vehicleNo)
        {
            foreach (var pair in _data.ToList())
            {
                var v = pair.Value.Find(x => x.Id == vehicleNo);
                if (v != null)
                {
                    pair.Value.Remove(v);
                    return;
                }
            }

            throw new ScenarioException("Violation Not Found");
        }
    }
}
