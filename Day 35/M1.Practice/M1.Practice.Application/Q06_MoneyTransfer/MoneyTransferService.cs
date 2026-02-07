using System;
using System.Collections.Generic;
using M1.Practice.Domain.Q06_MoneyTransfer;

namespace M1.Practice.Application.Q06_MoneyTransfer
{
    public class MoneyTransferService
    {
        private readonly object _lock = new object();

        private readonly Dictionary<string, Account> _accounts;

        private readonly List<string> _auditLog =
            new List<string>();

        public MoneyTransferService(
            Dictionary<string, Account> accounts)
        {
            _accounts = accounts;
        }

        // ---------------------------------------

        public TransferResult Transfer(
            string fromAcc,
            string toAcc,
            decimal amount)
        {
            Validate(fromAcc, toAcc, amount);

            lock (_lock)
            {
                var from = _accounts[fromAcc];
                var to = _accounts[toAcc];

                if (from.Balance < amount)
                    throw new InvalidTransferException(
                        "Insufficient balance");

                try
                {
                    // Debit
                    from.Balance -= amount;

                    // Credit (simulate possible failure)
                    Credit(to, amount);

                    LogSuccess(fromAcc, toAcc, amount);

                    return new TransferResult
                    {
                        IsSuccess = true,
                        Message = "Transfer successful"
                    };
                }
                catch (Exception ex)
                {
                    // Rollback
                    from.Balance += amount;

                    LogFailure(fromAcc, toAcc, amount, ex);

                    return new TransferResult
                    {
                        IsSuccess = false,
                        Message = "Transfer failed"
                    };
                }
            }
        }

        // ---------------------------------------

        private void Credit(Account acc, decimal amount)
        {
            // Simulate random failure
            if (new Random().Next(1, 5) == 1)
                throw new Exception("Credit failed");

            acc.Balance += amount;
        }

        // ---------------------------------------

        private void Validate(
            string from,
            string to,
            decimal amount)
        {
            if (string.IsNullOrEmpty(from) ||
                string.IsNullOrEmpty(to))
                throw new InvalidTransferException(
                    "Account missing");

            if (amount <= 0)
                throw new InvalidTransferException(
                    "Invalid amount");

            if (!_accounts.ContainsKey(from) ||
                !_accounts.ContainsKey(to))
                throw new InvalidTransferException(
                    "Account not found");
        }

        // ---------------------------------------

        private void LogSuccess(
            string from,
            string to,
            decimal amt)
        {
            _auditLog.Add(
                $"SUCCESS {from}->{to} {amt}");
        }

        private void LogFailure(
            string from,
            string to,
            decimal amt,
            Exception ex)
        {
            _auditLog.Add(
                $"FAIL {from}->{to} {amt} : {ex.Message}");
        }

        // ---------------------------------------

        public List<string> GetAuditLog()
        {
            return _auditLog;
        }
    }
}
