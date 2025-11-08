using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Transactions;


class Program
{
    static void Main(string[] args)
    {
        BankManager.LoadFromFile();

        bool keepGoing = true;
        while (keepGoing)
        {
            Console.WriteLine("\n=== Bank Account Simulator ===");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. View All Accounts");
            Console.WriteLine("3. Select Account");
            Console.WriteLine("4. Delete Account");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    BankManager.CreateAccount();
                    break;
                case "2":
                    BankManager.ViewAllAccounts();
                    break;
                case "3":
                    SelectAccountMenu();
                    break;
                case "4":
                    BankManager.DeleteAccount();
                    break;
                case "5":
                    keepGoing = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    static void SelectAccountMenu()
    {
        BankManager.ViewAllAccounts();
        Console.Write("\nEnter account number: ");
        
        if (int.TryParse(Console.ReadLine(), out int accountNumber))
        {
            Account? account = BankManager.FindAccount(accountNumber);
            
            if (account != null)
            {
                AccountMenu(account);
            }
            else
            {
                Console.WriteLine("Account not found!");
            }
        }
        else
        {
            Console.WriteLine("Invalid account number!");
        }
    }

    static void AccountMenu(Account account)
    {
        bool backToMain = false;
        while (!backToMain)
        {
            Console.WriteLine($"\n=== Account: {account.AccountHolderName} ({account.AccountNumber}) ===");
            Console.WriteLine($"Balance: ${account.Balance}");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. View Transaction History");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    Console.Write("Enter amount to deposit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        account.Deposit(depositAmount);
                        BankManager.SaveToFile();
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount!");
                    }
                    break;
                case "2":
                    Console.Write("Enter amount to withdraw: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                    {
                        account.Withdraw(withdrawAmount);
                        BankManager.SaveToFile();
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount!");
                    }
                    break;
                case "3":
                    account.ViewTransactionHistory();
                    break;
                case "4":
                    backToMain = true;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}
class Account
{
    public int AccountNumber { get; set; }
    public string AccountHolderName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public Account(int accountNumber, string accountHolderName, decimal balance = 0)
    {
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            this.Balance += amount;
            Transactions.Add(new Transaction(DateTime.Now, "Deposit", amount, Balance));
        }
        else
        {
            Console.WriteLine("Invalid amount!");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Amount must be positive!");
            return;
        }

        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds!");
            return;
        }

        Balance -= amount;
        Transactions.Add(new Transaction(DateTime.Now, "Withdraw", amount, Balance));
        Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");


    }
    public void ViewTransactionHistory()
    {
        if (Transactions.Count == 0)
        {
            Console.WriteLine("No transactions yet!");
        }
        else
        {
            foreach (Transaction transaction in Transactions)
            {
                Console.WriteLine($"Date/Time: {transaction.Date}\nType: {transaction.Type}\nAmount: {transaction.Amount}\nBalance after: {transaction.BalanceAfter}");
            }
        }

    }
}

class Transaction
{
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }

    public Transaction(DateTime date, string type, decimal amount, decimal balanceAfter)
    {
        Date = date;
        Type = type;
        Amount = amount;
        BalanceAfter = balanceAfter;

    }
}

class BankManager
{
    static List<Account> accounts = new List<Account>();

    public static void CreateAccount()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        int nextNumber = accounts.Count + 1;
        int accountNumber = 999 + nextNumber;

        Account newAccount;

        Console.WriteLine("Would you like to deposit anything right away? (y/n)");
        string ifDeposit = Console.ReadLine() ?? "";
        switch (ifDeposit.ToLower())
        {
            case "y":
                Console.Write("Amount to deposit: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal balance))
                {
                    newAccount = new Account(accountNumber, name, balance);
                    Console.WriteLine($"Account created! Name: {name},\naccount number: {accountNumber},\nbalance: {balance}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Creating account with 0$ balance.");
                    newAccount = new Account(accountNumber, name);
                    break;
                }

            case "n":
                newAccount = new Account(accountNumber, name);
                Console.WriteLine($"Account created! Name: {name},\nAccount number {accountNumber}");
                break;
            default:
                Console.WriteLine("Invalid input. Creating account with 0$ balance.");
                newAccount = new Account(accountNumber, name);
                break;
        }
        
        accounts.Add(newAccount);
        

        SaveToFile();



    }
    public static Account? FindAccount(int accountNumber)
    {
        return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }
    public static void ViewAllAccounts()
    {
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts yet!");
        }
        else
        {
            foreach (Account account in accounts)
            {
                Console.WriteLine($"Name: {account.AccountHolderName}\nNumber: {account.AccountNumber}\nBalance:{account.Balance}");
            }
        }
    }

    public static void DeleteAccount()
    {
        ViewAllAccounts();
        Console.WriteLine("Account number to delete: ");

        if (int.TryParse(Console.ReadLine(), out int accountNumber))
        {

            Account? accountToDelete = FindAccount(accountNumber);

            if (accountToDelete != null)
            {
                accounts.Remove(accountToDelete);
                Console.WriteLine($"Account {accountNumber} deleted!");
                SaveToFile();
            }
            else
            {
                Console.WriteLine("That account doesent exist!");
            }
        }
        else
        {
            Console.WriteLine("Invalid account number!");
        }
    }

    public static void SaveToFile()
    {
        try
        {
            string fileName = "accounts.json";
            string jsonString = JsonSerializer.Serialize(accounts);
            File.WriteAllText(fileName, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while saving accounts! {ex.Message}");
        }
    }
    public static void LoadFromFile()
    {
        try
        {
            string fileName = "accounts.json";
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                accounts = JsonSerializer.Deserialize<List<Account>>(jsonString) ?? new List<Account>();
            }
            else
            {
                Console.WriteLine("No saved accounts found, starting fresh!");
            }
        }
        catch (JsonException)
        {
            Console.WriteLine("File corrupted, starting fresh!");
            accounts = new List<Account>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            accounts = new List<Account>();

        }
        
    }
}