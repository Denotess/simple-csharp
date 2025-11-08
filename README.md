
# C# Learning Projects

This repository contains several small, intentionally simple C# console applications. They're learning projects — short exercises to practice building apps with .NET and to show concepts for others who want to learn by doing.

Included projects
- `calculator/` — a basic calculator console app (arithmetic practice).
- `contacts/` — an original minimal contacts manager (simple I/O and data handling).
- `contactsv2/` — an upgraded contact manager that loads/saves `contacts.json` with basic error handling.
- `toDoList/` — a simple to-do list console app (state management and optional persistence exercises).
- `bankAccount/` — a small bank account simulator with transactions and `accounts.json` persistence.

Prerequisites
- .NET 9 SDK (or a compatible .NET 9 runtime + SDK). Verify with:

```bash
dotnet --version
```

Build & run
Build all projects from the repo root:

```bash
dotnet build
```

Run an individual project from the repo root. If the project folder contains a .csproj file you can run it directly, for example:

```bash
dotnet run --project toDoList
dotnet run --project contactsv2
dotnet run --project bankAccount
```

If a folder only contains a single `Program.cs`, follow the project README inside that folder for instructions on creating a console project and copying the file in.

Notes
- These projects are learning examples and not intended for production use.
- No license file is included intentionally — these examples are for learning and demonstration.

How to contribute
- Fork the repo and send a pull request with small improvements or new learning exercises.
- Keep changes small and focused (one idea per PR).

Contact
- Open an issue in this repository for questions or suggestions.

