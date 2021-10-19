using System;

namespace BankingApplication.Errors
{
    class InvalidInvestmentTypeException : Exception
    {
        public InvalidInvestmentTypeException() : base("Investment type must be either Individual or Corporate") { }
    }
}
