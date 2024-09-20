using System;

public class Account
{
    private string _name;
    private decimal _balance;

    public Account(string name, decimal balance)
    {
        _name = name;
        _balance = balance;
    }

    public bool Deposit(decimal amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            Console.WriteLine("You have successfully deposited " + amount.ToString("C") + ".");
            return true;
        }
        else
        {
            Console.WriteLine("Unsuccessful deposit. Deposit must be greater than zero.");
            return false;
        }
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Invalid withdrawl amount. Withdrawal must be greater than zero.");
            return false;
        }
        else if (amount <= _balance)
        {
            _balance -= amount;
            Console.WriteLine("You have withdrawn " + amount.ToString("C") + ".");
            return true;
        }
        else
        {
            Console.WriteLine("Insufficient funds. Cannot process withdrawal.");
            return false;
        }
    }

    public void Print()
    {
        Console.WriteLine("Account name: " + _name + "\nBalance: " + _balance);
    }

    public string name
    {
        get { return _name; }
    }
}