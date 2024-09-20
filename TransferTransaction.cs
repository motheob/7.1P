using System;

public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private WithdrawTransaction _withdrawTransaction;
    private DepositTransaction _depositTransaction;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
        : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _withdrawTransaction = new WithdrawTransaction(fromAccount, amount);
        _depositTransaction = new DepositTransaction(toAccount, amount);
    }

    public override bool Success => _withdrawTransaction.Success && _depositTransaction.Success;

    public override void Execute()
    {
        if (_executed)
        {
            throw new InvalidOperationException("Transaction has already been executed.");
        }

        _withdrawTransaction.Execute();
        if (!_withdrawTransaction.Success)
        {
            throw new InvalidOperationException("Withdrawal failed in transfer.");
        }

        _depositTransaction.Execute();
        if (!_depositTransaction.Success)
        {
            _withdrawTransaction.Rollback();
            throw new InvalidOperationException("Deposit failed in transfer.");
        }

        _executed = true;
        _datestamp = DateTime.Now;
    }

    public override void Rollback()
    {
        base.Rollback();
        if (_depositTransaction.Success)
        {
            _depositTransaction.Rollback();
        }
        if (_withdrawTransaction.Success)
        {
            _withdrawTransaction.Rollback();
        }
    }

    public override void Print()
    {
        Console.WriteLine("Transfer: From " + _fromAccount.name + " To " + _toAccount.name);
        Console.WriteLine("Amount: " + _amount);
        Console.WriteLine("Executed: " + _executed);
        Console.WriteLine("Reversed: " + _reversed);
        Console.WriteLine("Date: " + _datestamp);
    }
}