using System;
using System.Collections.Generic;

public class Bank
{
    private List<Account> _accounts = new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account GetAccount(string name)
    {
        return _accounts.Find(a => a.name == name);
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction); // Add transaction to the list
    }

    public void RollbackTransaction(Transaction transaction)
    {
        transaction.Rollback();
    }

    public void PrintTransactionHistory()
    {
        if (_transactions.Count == 0)
        {
            Console.WriteLine("No transactions available.");
        }
        else
        {
            Console.WriteLine("Transaction History:");
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.Write(i + 1);
                _transactions[i].Print();
            }
        }
    }

    public int TransactionCount
    {
        get { return _transactions.Count; }
    }

    public Transaction GetTransaction(int index)
    {
        if (index >= 0 && index < _transactions.Count)
        {
            return _transactions[index];
        }
        else
        {
            throw new ArgumentOutOfRangeException("Invalid transaction index.");
        }
    }
}