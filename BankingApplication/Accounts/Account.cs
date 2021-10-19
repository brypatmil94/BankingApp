using BankingApplication.Errors;
using System;

namespace BankingApplication
{
    public abstract class Account
    {
        public Guid AccountId { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }

        public Account(string ownerName, string accountType)
        {
            AccountId = Guid.NewGuid();
            Owner = ownerName;
            Balance = 0;
            AccountType = accountType;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (amount >= Balance)
            {
                throw new OverdraftException();
            };

            Balance -= amount;
        }

        public void Transfer(Account account, decimal amount)
        {
            if (account.AccountId == AccountId)
            {
                throw new SameAccountException();
            }

            Withdrawal(amount);
            account.Deposit(amount);
        }
    }
}
