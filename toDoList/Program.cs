using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        bool keepGoing = true;
        while (keepGoing)
        {
            Console.WriteLine("What action would you like to do? (view, add, remove, exit)");
            string choice = Console.ReadLine() ?? "";
            switch (choice.ToLower())
            {
                case "view":
                    ToDo.ViewTasks();
                    break;
                case "add":
                    ToDo.AddTask();
                    break;
                case "remove":
                    ToDo.DeleteTask();
                    break;
                case "exit":
                    keepGoing = false;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

    }
}

class ToDo
{
    static List<string> tasks = new List<string>();
    public static void ViewTasks()
    {
        int length = tasks.Count;
        if (length < 1)
        {
            Console.WriteLine("Theres no tasks in the To-Do list");
        }
        else
        {
            int i = 1;
            foreach (string task in tasks)
            {
                Console.WriteLine($"{i}. {task}");
                i++;
            }
        }
    }

    public static void AddTask()
    {
        string task;
        Console.Write("Enter the task you want to add: ");
        task = Console.ReadLine() ?? "";
        while (task.Length < 1)
        {
            Console.WriteLine("The task is too short, try again");
            task = Console.ReadLine() ?? ""; 

        }
        tasks.Add(task);
        Console.WriteLine($"Added task: {task}");
    }
    public static void DeleteTask()
    {
        int index;
        Console.Write("Enter the number of the task you would like to delete: ");
        index =  Int32.Parse(Console.ReadLine() ?? "-1") - 1;
        if (index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("That task does not exist");
        }
        else
        {
            tasks.RemoveAt(index);
            Console.WriteLine($"{index + 1}. task deleted.");
        }
    }
}