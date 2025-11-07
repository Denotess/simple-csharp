
# C# Learning Projects

This repository contains a few small, intentionally simple C# console applications. They're learning projects small exercises to practice building apps with .NET and to show concepts for others who want to learn by doing.

Included projects
- `calculator/` — a basic calculator console app (arithmetic practice).
- `contacts/` — a minimal contacts manager (simple I/O and data handling).
- `toDoList/` — a simple to-do list console app (state management and persistence exercises if implemented).

What I'm learning / intent
- Using the .NET SDK and C# to create small console applications.
- Project structure and simple build/run workflows.
- Basic program design, input/output handling, and small-scale state management.

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

Run an individual project from the repo root, for example the to-do list project:

```bash
dotnet run --project toDoList/toDoList.csproj
```

Swap the path for `calculator/calculator.csproj` or `contacts/contacts.csproj` to run the other apps.

Notes
- These projects are small learning examples and not intended for production use.
- No license file is included intentionally — these examples are for learning and demonstration.

How to contribute (if you'd like to help)
- Fork the repo and send a pull request with simple improvements or exercises.
- Keep changes small and focused (one learning idea per PR).

Contact
- Open an issue in this repository for questions or suggestions.

