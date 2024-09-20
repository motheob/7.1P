using System;

namespace _7._1P
{
    public enum MenuOption
    {
        AddNewAccount = 1,
        Withdraw,
        Deposit,
        Print,
        Transfer,
        PrintTransactionHistory,
        Quit
    }

    class Program
    {
        public static MenuOption ReadUserOption()
        {
            int choice;
            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add new account");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Print");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Print Transaction History");
                Console.WriteLine("7. Quit");
                Console.Write("Please select an option: ");

                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                }
            }
            while (choice < 1 || choice > 7);

            MenuOption option = (MenuOption)choice;
            return option;
        }

        public static void DoDeposit(Bank bank)
        {
            var account = FindAccount(bank);
            if (account != null)
            {
                Console.Write("Enter amount to deposit: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var transaction = new DepositTransaction(account, amount);
                    bank.ExecuteTransaction(transaction);

                    Console.WriteLine(transaction.Success ? "Deposit successful." : "Deposit failed.");
                    transaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
        }

        public static void DoWithdraw(Bank bank)
        {
            var account = FindAccount(bank);
            if (account != null)
            {
                Console.Write("Enter amount to withdraw: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var transaction = new WithdrawTransaction(account, amount);
                    bank.ExecuteTransaction(transaction);

                    Console.WriteLine(transaction.Success ? "Withdrawal successful." : "Withdrawal failed.");
                    transaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
        }

        public static void DoPrint(Bank bank)
        {
            var account = FindAccount(bank);
            if (account != null)
            {
                account.Print();
            }
        }

        public static void DoTransfer(Bank bank)
        {
            Console.Write("Enter debit account name: ");
            string fromName = Console.ReadLine();
            var fromAccount = bank.GetAccount(fromName);

            Console.Write("Enter credit account name: ");
            string toName = Console.ReadLine();
            var toAccount = bank.GetAccount(toName);

            if (fromAccount != null && toAccount != null)
            {
                Console.Write("Enter amount to transfer: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var transaction = new TransferTransaction(fromAccount, toAccount, amount);
                    bank.ExecuteTransaction(transaction);

                    Console.WriteLine(transaction.Success ? "Transfer successful." : "Transfer failed.");
                    transaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
            else
            {
                Console.WriteLine("One or both accounts not found.");
            }
        }

        public static void AddNewAccount(Bank bank)
        {
            Console.WriteLine("Enter account name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter a starting balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                var account = new Account(name, balance);
                bank.AddAccount(account);
                Console.WriteLine("Account has been successfully created.");
            }
            else
            {
                Console.WriteLine("Invalid balance amount.");
            }
        }

        private static Account FindAccount(Bank bank)
        {
            Console.WriteLine("Enter account name: ");
            string name = Console.ReadLine();
            var account = bank.GetAccount(name);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
            }

            return account;
        }

        public static void DoPrintTransactionHistory(Bank bank)
        {
            bank.PrintTransactionHistory();
            Console.Write("Would you like to rollback a transaction? (Enter index or 'n' to skip): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index > 0 && index <= bank.TransactionCount)
            {
                try
                {
                    bank.RollbackTransaction(bank.GetTransaction(index - 1));
                    Console.WriteLine("Rollback successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rollback failed: " + ex.Message);
                }
            }
            else if (input.ToLower() != "n")
            {
                Console.WriteLine("Invalid input. No rollback performed.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to MB Bank!");

            var bank = new Bank();

            MenuOption selectedOption;
            do
            {
                selectedOption = ReadUserOption();

                switch (selectedOption)
                {
                    case MenuOption.AddNewAccount:
                        AddNewAccount(bank);
                        break;

                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOption.Print:
                        DoPrint(bank);
                        break;

                    case MenuOption.Transfer:
                        DoTransfer(bank);
                        break;

                    case MenuOption.PrintTransactionHistory:
                        DoPrintTransactionHistory(bank);
                        break;

                    case MenuOption.Quit:
                        Console.WriteLine("Closing Application...");
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please choose a valid option.");
                        break;
                }
            }
            while (selectedOption != MenuOption.Quit);

            Console.WriteLine("Thank you for using MB Bank.");
        }
    }
}