using BankingApplication.Accounts;
using BankingApplication.Errors;
using FluentAssertions;
using Xunit;

namespace BankingUnitTests
{
    public class CheckingTests
    {
        [Fact]
        public void CheckingShouldStartWithZeroBalance()
        {
            var checking1 = new Checking("Bob");

            checking1.Balance.Should().Be(0);
        }

        [Fact]
        public void DepositToCheckingAddsDesiredAmount()
        {
            var checking1 = new Checking("Bob");

            checking1.Deposit(10);

            checking1.Balance.Should().Be(10);
        }

        [Fact]
        public void WithdrawalFromCheckingSubtractsDesiredAmount()
        {
            var checking1 = new Checking("Bob");

            checking1.Deposit(100);

            checking1.Balance.Should().Be(100);

            checking1.Withdrawal(50);

            checking1.Balance.Should().Be(50);
        }

        [Fact]
        public void OverWithdrawalFromCheckingThrowsOverdraftException()
        {
            var checking1 = new Checking("Bob");

            checking1.Deposit(10);

            checking1.Invoking(s => s.Withdrawal(50))
                .Should().Throw<OverdraftException>()
                .WithMessage("Cannot withraw more than balance");

            checking1.Balance.Should().Be(10);
        }

        [Fact]
        public void TransferFromOneCheckingToAnotherShouldLowerOriginalAndRaiseRecieving()
        {
            var checking1 = new Checking("Bob");
            var checking2 = new Checking("Sam");

            checking1.Deposit(30);

            checking1.Transfer(checking2, 10);

            checking1.Balance.Should().Be(20);
            checking2.Balance.Should().Be(10);
        }

        [Fact]
        public void TransferToSameCheckingShouldThrowSameAccountException()
        {
            var checking1 = new Checking("Bob");

            checking1.Deposit(30);

            checking1.Invoking(c => c.Transfer(checking1, 10))
                .Should().Throw<SameAccountException>()
                .WithMessage("Cannot transfer to same account");

            checking1.Balance.Should().Be(30);
        }
    }
}
