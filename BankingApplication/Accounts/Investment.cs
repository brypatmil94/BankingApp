using BankingApplication.Errors;

namespace BankingApplication.Accounts
{
    public class Investment : Account
    {
        public string InvestmentType { get; set; }
        public Investment(string ownerName, string investmentType) : base(ownerName, "Investment")
        {
            if (investmentType == "Individual" || investmentType == "Corporate")
            {
                InvestmentType = investmentType;
            }
            else
            {
                throw new InvalidInvestmentTypeException();
            }
        }

        public void Withdraw(decimal amount)
        {
            if (InvestmentType == "Individual" && amount > 500)
            {
                throw new IndividualInvestmentWithdrawalException();
            }
            Balance -= amount;
        }
    }
}
