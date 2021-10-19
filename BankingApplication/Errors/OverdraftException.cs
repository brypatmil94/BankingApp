using System;

namespace BankingApplication.Errors
{
    public class OverdraftException : Exception
    {
        public OverdraftException() : base("Cannot withraw more than balance") {}
    }
}
