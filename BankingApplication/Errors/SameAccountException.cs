using System;

namespace BankingApplication.Errors
{
    public class SameAccountException : Exception
    {
        public SameAccountException() : base("Cannot transfer to same account") {}
    }
}
