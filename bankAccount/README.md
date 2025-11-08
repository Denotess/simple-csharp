# Bank Account Simulator

A small learning project that simulates simple bank accounts. This console app lets you create accounts, deposit and withdraw money, view transaction history, and delete accounts. Data is persisted to `accounts.json` and basic error handling is included for missing or corrupted files.

What it does
- Create accounts with a generated account number and optional initial deposit.
- View all accounts and select a specific account to interact with.
- Deposit and withdraw money (with checks for invalid amounts and insufficient funds).
- Record transactions (date/time, type, amount, balance after) and display transaction history per account.
- Persist accounts and transactions to `accounts.json` using System.Text.Json.
- Recover from missing or corrupted JSON files by starting with an empty state and reporting the issue.

Data shape (example `accounts.json`)

```json
[
  {
    "AccountNumber": 1000,
    "AccountHolderName": "Alice Example",
    "Balance": 150.0,
    "Transactions": [
      {
        "Date": "2025-11-08T12:34:56",
        "Type": "Deposit",
        "Amount": 150.0,
        "BalanceAfter": 150.0
      }
    ]
  }
]
```

Prerequisites
- .NET 9 SDK (or a compatible .NET 9 runtime + SDK).

Check your SDK version:

```bash
dotnet --version
```

Build & run
- From the repository root you can build and run the project folder:

```bash
dotnet build bankAccount
dotnet run --project bankAccount
```

- If `bankAccount` is not a complete project folder but only contains `Program.cs`, create a console project and copy the file in:

```bash
mkdir demo && cd demo
dotnet new console
# replace demo/Program.cs with ../bankAccount/Program.cs
dotnet run
```

Notes & behaviour
- The app saves changes to `accounts.json` whenever accounts are created, deposits/withdrawals are made, or accounts are deleted.
- `accounts.json` is read from and written to the current working directory. If you run with `--project bankAccount` from the repo root, it will create/read `./bankAccount/accounts.json`.
- If the file is missing the program starts fresh and informs you. If the file is corrupted the program logs the issue and starts with an empty account list to avoid crashes.
- Transactions are stored per account; each transaction records date/time, type, amount, and the resulting balance.

Quick menu reference
- 1 Create Account
- 2 View All Accounts
- 3 Select Account (enter account number to deposit/withdraw/view history)
- 4 Delete Account
- 5 Exit