using System;

namespace BankingApplication.Errors
{
    public class IndividualInvestmentWithdrawalException : Exception
    {
        public IndividualInvestmentWithdrawalException() : base("Individual investment accounts cannot withdraw more than $500") { }
    }
}
