using System.Collections.Generic;

namespace BankingApplication
{
    public class Bank
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public void CloseAccount(Account account)
        {
            Accounts.Remove(account);
        }
    }
}
