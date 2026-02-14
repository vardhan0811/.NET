using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = Balance (auto sorted ascending)
        private SortedDictionary<decimal, List<Account>> _data =
            new SortedDictionary<decimal, List<Account>>();

        public void AddAccount(Account acc)
        {
            acc.Validate();

            // Duplicate Account Check
            foreach (var list in _data.Values)
            {
                if (list.Exists(a => a.Id == acc.Id))
                    throw new ScenarioException("Duplicate Account");
            }

            if (!_data.ContainsKey(acc.Balance))
                _data[acc.Balance] = new List<Account>();

            _data[acc.Balance].Add(acc);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            foreach (var pair in _data)
                foreach (var acc in pair.Value)
                    yield return acc;
        }

        public void Deposit(string id, decimal amount)
        {
            if (amount <= 0)
                throw new ScenarioException("Invalid Deposit Amount");

            foreach (var pair in _data.ToList())
            {
                var acc = pair.Value.Find(a => a.Id == id);
                if (acc != null)
                {
                    pair.Value.Remove(acc);

                    acc.Balance += amount;

                    if (!_data.ContainsKey(acc.Balance))
                        _data[acc.Balance] = new List<Account>();

                    _data[acc.Balance].Add(acc);
                    return;
                }
            }

            throw new ScenarioException("Account Not Found");
        }

        public void Withdraw(string id, decimal amount)
        {
            if (amount <= 0)
                throw new ScenarioException("Invalid Withdraw Amount");

            foreach (var pair in _data.ToList())
            {
                var acc = pair.Value.Find(a => a.Id == id);
                if (acc != null)
                {
                    if (acc.Balance < amount)
                        throw new ScenarioException("Insufficient Funds");

                    pair.Value.Remove(acc);

                    acc.Balance -= amount;

                    if (!_data.ContainsKey(acc.Balance))
                        _data[acc.Balance] = new List<Account>();

                    _data[acc.Balance].Add(acc);
                    return;
                }
            }

            throw new ScenarioException("Account Not Found");
        }
    }
}
