using System.Collections;

class Calculator
{
    public static double Add(double num1, double num2)
    {
        return num1 + num2;
    }
    public static double Subtract(double num1, double num2)
    {
        return num1 - num2;
    }
    public static double Multiply(double num1, double num2)
    {
        return num1 * num2;
    }
    public static double Divide(double num1, double num2)
    {
        return num1 / num2;
    }
}

class Program
{
    static void Main(string[] args)
    {
        double num1;
        double num2;
        double result;
        string operation;
        bool loop = true;
        Console.WriteLine("Calculator");
        while (loop)
        {
            Console.Write("First number: ");
            num1 = Convert.ToDouble(Console.ReadLine() ?? "");
            Console.Write("Second number: ");
            num2 = Convert.ToDouble(Console.ReadLine() ?? "");
            Console.Write("Choose operation (+, -, *, /): ");
            operation = Console.ReadLine() ?? "";
            switch (operation)
            {
                case "+":
                    result = Calculator.Add(num1, num2);
                    Console.WriteLine($"{num1} {operation} {num2} = {result}");
                    break;
                case "-":
                    result = Calculator.Subtract(num1, num2);
                    Console.WriteLine($"{num1} {operation} {num2} = {result}");
                    break;
                case "*":
                    result = Calculator.Multiply(num1, num2);
                    Console.WriteLine($"{num1} {operation} {num2} = {result}");
                    break;
                case "/":
                    result = Calculator.Divide(num1, num2);
                    Console.WriteLine($"{num1} {operation} {num2} = {result}");
                    break;
            }
            Console.Write("Keep going? (y/n)");
            string? answer = Console.ReadLine(); 
            if ( answer == "n" || answer == "N"){
                loop = false;
                Console.WriteLine("Thank you for using the calculator");
            }
        }
    }
}