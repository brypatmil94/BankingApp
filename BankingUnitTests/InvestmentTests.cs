using BankingApplication.Accounts;
using BankingApplication.Errors;
using FluentAssertions;
using Xunit;

namespace BankingUnitTests
{
    public class InvestmentTests
    {
        private const string CORPORATE = "Corporate";
        private const string INDIVIDUAL = "Individual";

        [Fact]
        public void InvestmentShouldStartWithZeroBalance()
        {
            var investment1 = new Investment("Bob", CORPORATE);

            investment1.Balance.Should().Be(0);
        }

        [Fact]
        public void DepositToInvestmentAddsDesiredAmount()
        {
            var investment1 = new Investment("Bob", CORPORATE);

            investment1.Deposit(10);

            investment1.Balance.Should().Be(10);
        }

        [Fact]
        public void WithdrawalFromInvestmentSubtractsDesiredAmount()
        {
            var investment1 = new Investment("Bob", CORPORATE);

            investment1.Deposit(100);

            investment1.Withdrawal(50);

            investment1.Balance.Should().Be(50);
        }

        [Fact]
        public void OverWithdrawalFromInvestmentThrowsOverdraftException()
        {
            var investment1 = new Investment("Bob", CORPORATE);

            investment1.Deposit(10);

            investment1.Invoking(c => c.Withdrawal(50))
                .Should().Throw<OverdraftException>()
                .WithMessage("Cannot withraw more than balance");

            investment1.Balance.Should().Be(10);
        }

        [Fact]
        public void TransferFromOneInvestmentToAnotherShouldLowerOriginalAndRaiseRecieving()
        {
            var investment1 = new Investment("Bob", CORPORATE);
            var investment2 = new Investment("Sam", CORPORATE);

            investment1.Deposit(30);

            investment1.Transfer(investment2, 10);

            investment1.Balance.Should().Be(20);
            investment2.Balance.Should().Be(10);
        }

        [Fact]
        public void TransferToSameInvestmentShouldThrowSameAccountException()
        {
            var investment1 = new Investment("Bob", CORPORATE);

            investment1.Deposit(30);

            investment1.Invoking(c => c.Transfer(investment1, 10))
                .Should().Throw<SameAccountException>()
                .WithMessage("Cannot transfer to same account");

            investment1.Balance.Should().Be(30);
        }

        [Fact]
        public void IndividualInvestmentCannotWithdrawMoreThan500()
        {
            var investment1 = new Investment("Bob", INDIVIDUAL);

            investment1.Deposit(3000);

            investment1.Invoking(c => c.Withdraw(2000))
                .Should().Throw<IndividualInvestmentWithdrawalException>()
                .WithMessage("Individual investment accounts cannot withdraw more than $500");

            investment1.Balance.Should().Be(3000);
        }

        [Fact]
        public void MoneyMarketInvestmentCanWithdrawMoreThan500()
        {
            var investment1 = new Investment("Bob", CORPORATE);

            investment1.Deposit(3000);

            investment1.Withdraw(2000);

            investment1.Balance.Should().Be(1000);
        }
    }
}
