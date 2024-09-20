using System;

public class WithdrawTransaction : Transaction
{
    private Account _account;

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override bool Success => _executed && !_reversed;

    public override void Execute()
    {
        if (_executed)
        {
            throw new InvalidOperationException("Transaction has already been executed.");
        }

        _executed = _account.Withdraw(_amount);
        if (!_executed)
        {
            throw new InvalidOperationException("Withdrawal failed.");
        }
        _datestamp = DateTime.Now;
    }

    public override void Print()
    {
        Console.WriteLine("Withdrawal: Amount " + _amount);
        Console.WriteLine("Executed: " + _executed);
        Console.WriteLine("Reversed: " + _reversed);
        Console.WriteLine("Date: " + _datestamp);
    }
}