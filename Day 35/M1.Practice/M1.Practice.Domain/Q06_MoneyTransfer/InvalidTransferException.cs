using System;

namespace M1.Practice.Domain.Q06_MoneyTransfer
{
    public class InvalidTransferException : Exception
    {
        public InvalidTransferException(string message)
            : base(message)
        {
        }
    }
}
