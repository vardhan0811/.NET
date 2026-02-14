using System.Collections.Generic;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        private SortedDictionary<int, List<Medicine>> _data =
            new SortedDictionary<int, List<Medicine>>();

        public void AddMedicine(Medicine med)
        {
            // Duplicate check
            foreach (var list in _data.Values)
            {
                if (list.Exists(m => m.Id == med.Id))
                    throw new ScenarioException("Duplicate Medicine");
            }

            if (!_data.ContainsKey(med.ExpiryYear))
                _data[med.ExpiryYear] = new List<Medicine>();

            _data[med.ExpiryYear].Add(med);
        }

        public IEnumerable<Medicine> GetAll()
        {
            foreach (var year in _data)
                foreach (var med in year.Value)
                    yield return med;
        }

        public void UpdatePrice(string id, int newPrice)
        {
            if (newPrice <= 0)
                throw new ScenarioException("Invalid Price");

            foreach (var list in _data.Values)
            {
                var med = list.Find(m => m.Id == id);
                if (med != null)
                {
                    med.Price = newPrice;
                    return;
                }
            }

            throw new ScenarioException("Medicine Not Found");
        }
    }
}
