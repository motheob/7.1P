using System;

public abstract class Transaction
{
    protected decimal _amount;
    protected bool _executed;
    protected bool _reversed;
    protected DateTime _datestamp;

    public Transaction(decimal amount)
    {
        _amount = amount;
        _executed = false;
        _reversed = false;
    }

    public abstract void Execute();  

    public virtual void Rollback()
    {
        if (!_executed)
        {
            throw new InvalidOperationException("Transaction has not been executed.");
        }
        if (_reversed)
        {
            throw new InvalidOperationException("Transaction has already been reversed.");
        }

        _reversed = true;
        _datestamp = DateTime.Now;
    }

    public virtual bool Executed => _executed;
    public virtual bool Reversed => _reversed;
    public virtual decimal Amount => _amount;

    public abstract bool Success { get; }

    public abstract void Print();
}