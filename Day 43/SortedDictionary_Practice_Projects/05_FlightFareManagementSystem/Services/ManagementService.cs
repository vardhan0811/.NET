using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = Fare (Ascending Automatically)
        private SortedDictionary<int, List<Ticket>> _data =
            new SortedDictionary<int, List<Ticket>>();

        public void AddTicket(Ticket t)
        {
            t.Validate();

            // Duplicate Check
            foreach (var list in _data.Values)
            {
                if (list.Exists(x => x.Id == t.Id))
                    throw new ScenarioException("Duplicate Ticket");
            }

            if (!_data.ContainsKey(t.Fare))
                _data[t.Fare] = new List<Ticket>();

            _data[t.Fare].Add(t);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            foreach (var pair in _data)
                foreach (var t in pair.Value)
                    yield return t;
        }

        public void UpdateFare(string id, int newFare)
        {
            if (newFare <= 0)
                throw new ScenarioException("Invalid Fare");

            foreach (var pair in _data.ToList())
            {
                var ticket = pair.Value.Find(x => x.Id == id);
                if (ticket != null)
                {
                    pair.Value.Remove(ticket);

                    ticket.Fare = newFare;

                    if (!_data.ContainsKey(ticket.Fare))
                        _data[ticket.Fare] = new List<Ticket>();

                    _data[ticket.Fare].Add(ticket);
                    return;
                }
            }

            throw new ScenarioException("Ticket Not Found");
        }
    }
}
