using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = FineAmount
        private SortedDictionary<int, List<Member>> _data =
            new SortedDictionary<int, List<Member>>();

        public void AddMember(Member m)
        {
            m.Validate();

            // Duplicate Check
            foreach (var list in _data.Values)
            {
                if (list.Exists(x => x.Id == m.Id))
                    throw new ScenarioException("Duplicate Member");
            }

            if (!_data.ContainsKey(m.FineAmount))
                _data[m.FineAmount] = new List<Member>();

            _data[m.FineAmount].Add(m);
        }

        public IEnumerable<Member> GetAllMembers()
        {
            // Reverse → show highest fine first
            foreach (var pair in _data.Reverse())
                foreach (var m in pair.Value)
                    yield return m;
        }

        public void PayFine(string id, int amount)
        {
            if (amount <= 0)
                throw new ScenarioException("Invalid Payment");

            foreach (var pair in _data.ToList())
            {
                var member = pair.Value.Find(x => x.Id == id);
                if (member != null)
                {
                    pair.Value.Remove(member);

                    member.FineAmount -= amount;
                    if (member.FineAmount < 0)
                        member.FineAmount = 0;

                    if (!_data.ContainsKey(member.FineAmount))
                        _data[member.FineAmount] = new List<Member>();

                    _data[member.FineAmount].Add(member);
                    return;
                }
            }

            throw new ScenarioException("Member Not Found");
        }
    }
}
