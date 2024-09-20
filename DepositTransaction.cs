using System;

public class DepositTransaction : Transaction
{
    private Account _account;

    public DepositTransaction(Account account, decimal amount) : base(amount)
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

        _executed = _account.Deposit(_amount);
        if (!_executed)
        {
            throw new InvalidOperationException("Deposit failed.");
        }
        _datestamp = DateTime.Now;
    }

    public override void Print()
    {
        Console.WriteLine("Deposit: Amount " + _amount);
        Console.WriteLine("Executed: " + _executed);
        Console.WriteLine("Reversed: " + _reversed);
        Console.WriteLine("Date: " + _datestamp);
    }
}