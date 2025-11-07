# Contact Manager (contactsv2)

An upgraded version of the simple Contact Manager example. This version loads contacts from `contacts.json`, saves back to `contacts.json`, and includes basic error handling for corrupted or missing files.

What it does
- Add, list, search, and delete contacts from a console menu.
- Persists contacts to `contacts.json` using System.Text.Json.
- Handles missing or invalid JSON files and recovers by starting with an empty contact list.

Key behavior
- On start the program attempts to load `contacts.json` from the current working directory.
- When you add or delete a contact, changes are saved immediately to `contacts.json`.
- If the JSON file is missing the program starts fresh and informs you.
- If the JSON file is corrupted the program reports the problem and starts with an empty list (preserves safety over crashing).

Data shape (example `contacts.json`)

```json
[
  {
    "Name": "Alice",
    "Surname": "Smith",
    "PhoneNumber": "555-1234",
    "Email": "alice@example.com"
  }
]
```

Usage
- Prerequisite: .NET 9 SDK (or a compatible .NET 9 runtime + SDK).

Check your SDK version:

```bash
dotnet --version
```

Build & run
- If `contactsv2` is a project folder with a .csproj file, run it from the repository root:

```bash
dotnet build contactsv2
dotnet run --project contactsv2
```

- If you only have the single `Program.cs` file, create a small console project and move the file in, or run it inside an existing project. For example:

```bash
mkdir -p demo && cd demo
dotnet new console
# copy ../contactsv2/Program.cs into demo and replace Program.cs
dotnet run
```

Notes & tips
- The app stores `contacts.json` in the current working directory. If you run from the repo root it will create/read `/path/to/repo/contactsv2/contacts.json` if run with `--project contactsv2`.
- If you see an error about corrupted JSON, you can inspect or remove the `contacts.json` file to start fresh.
- This is a learning example — consider adding unit tests, input validation, or optional encryption for saved data as exercises.

Menu (quick reference)
- 1 Add Contact
- 2 View All Contacts
- 3 Search Contact
- 4 Delete Contact
- 5 Exit

If you'd like, I can add a short example run showing sample inputs/outputs, or add a tiny test for the load/save behavior. Which would you prefer?
# Contacts (learning project)

A minimal contacts manager console app for learning basic data handling in C#.

What it is
- Simple program demonstrating storing and listing contacts (console I/O and in-memory data structures).

Prerequisites
- .NET 9 SDK installed. Verify with `dotnet --version`.

Build & run
From the repository root:

```bash
dotnet build contacts
dotnet run --project contacts
```

Notes
- Intended as a small learning exercise; consider adding example inputs/outputs or simple persistence as an enhancement.
