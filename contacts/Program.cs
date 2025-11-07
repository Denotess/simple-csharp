using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        bool keepGoing = true;

        while (keepGoing)
        {
            Console.WriteLine("\n=== Contact Manager ===");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. View All Contacts");
            Console.WriteLine("3. Search Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    ContactManager.AddContact();
                    break;
                case "2":
                    ContactManager.ViewAllContacts();
                    break;
                case "3":
                    ContactManager.SearchContact();
                    break;
                case "4":
                    ContactManager.DeleteContact();
                    break;
                case "5":
                    keepGoing = false;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}

class Contact
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public Contact(string name, string surname, string phoneNumber, string email)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}

class ContactManager
{
    static List<Contact> contacts = new List<Contact>();
    public static void AddContact()
    {
        Console.Write("Contact name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Contact Surname: ");
        string surname = Console.ReadLine() ?? "";

        Console.Write("Contact phone number: ");
        string phoneNumber = Console.ReadLine() ?? "";

        Console.Write("Contact email: ");
        string email = Console.ReadLine() ?? "";

        contacts.Add(new Contact(name, surname, phoneNumber, email));
        Console.WriteLine("Contact added!");
    }
    public static void ViewAllContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("Theres no contacts yet");
        }
        else
        {
            int i = 1;
            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"{i}.");
                Console.WriteLine($"Name: {contact.Name} {contact.Surname}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine("");
            }
        }
    }
    public static void DeleteContact()
    {
        ViewAllContacts();
        Console.WriteLine("Which one would you like to delete (e.g. '1'):");
        int index = Int32.Parse(Console.ReadLine() ?? "0") - 1;
        if (index < 0 || index >= contacts.Count)
        {
            Console.WriteLine("That contact does not exist");
        }
        else
        {
            contacts.RemoveAt(index);
            Console.WriteLine($"Removed {index + 1}. contact.");
        }

    }
    public static void SearchContact()
    {
        Console.WriteLine("What name would you like to search for?: ");
        string search = Console.ReadLine() ?? "";

        bool found = false;

        foreach (Contact contact in contacts)
        {
            if (
                contact.Name.ToLower().Contains(search.ToLower()) ||
                contact.Surname.ToLower().Contains(search.ToLower())
            )
            {
                Console.WriteLine($"Name: {contact.Name} {contact.Surname}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine("");

                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No contacts found matching that search.");
        }
    }
}

