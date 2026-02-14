using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = Severity
        // Value = Queue (FIFO processing)
        private SortedDictionary<int, Queue<SupportTicket>> _data =
            new SortedDictionary<int, Queue<SupportTicket>>();

        public void AddTicket(SupportTicket t)
        {
            t.Validate();

            if (!_data.ContainsKey(t.SeverityLevel))
                _data[t.SeverityLevel] = new Queue<SupportTicket>();

            _data[t.SeverityLevel].Enqueue(t);
        }

        // Process next highest priority ticket
        public SupportTicket ProcessNext()
        {
            foreach (var pair in _data)
            {
                if (pair.Value.Count > 0)
                    return pair.Value.Dequeue();
            }

            throw new ScenarioException("No Tickets Available");
        }

        // Escalate = Move ticket to higher priority
        public void Escalate(string id)
        {
            foreach (var pair in _data.ToList())
            {
                var queue = pair.Value;
                int size = queue.Count;

                for (int i = 0; i < size; i++)
                {
                    var ticket = queue.Dequeue();

                    if (ticket.Id == id)
                    {
                        if (ticket.SeverityLevel == 1)
                            throw new ScenarioException("Already Highest Priority");

                        ticket.SeverityLevel--;

                        if (!_data.ContainsKey(ticket.SeverityLevel))
                            _data[ticket.SeverityLevel] = new Queue<SupportTicket>();

                        _data[ticket.SeverityLevel].Enqueue(ticket);
                        return;
                    }

                    queue.Enqueue(ticket);
                }
            }

            throw new ScenarioException("Ticket Not Found");
        }

        public IEnumerable<SupportTicket> GetAll()
        {
            foreach (var pair in _data)
                foreach (var t in pair.Value)
                    yield return t;
        }
    }
}
