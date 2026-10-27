
using System;
using System.IO;

class Calculator
{
    static void Main()
    {
        string logPath = "./history.txt";

        Console.WriteLine("Enter your first number:");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter your second number:");
        double num2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter your operator (+, -, *, /):");
        string op = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(op))
        {
            Console.WriteLine("The operator is empty.");
            return;
        }

        double result = 0;
        string logMessage = "";

        switch (op)
        {
            case "+":
                result = num1 + num2;
                logMessage = $"{num1} + {num2} = {result}";
                break;

            case "-":
                result = num1 - num2;
                logMessage = $"{num1} - {num2} = {result}";
                break;

            case "*":
                result = num1 * num2;
                logMessage = $"{num1} * {num2} = {result}";
                break;

            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    logMessage = $"{num1} / {num2} = {result}";
                }
                else
                {
                    Console.WriteLine("Cannot divide by zero.");
                    return;
                }
                break;

            default:
                Console.WriteLine("Invalid operator.");
                return;
        }

        Console.WriteLine($"Result: {result}");

        string utcTimestamp = DateTime.UtcNow.ToString(
            "yyyy-MM-dd HH:mm:ss 'UTC'");

        using (StreamWriter sw = new StreamWriter(logPath, append: true))
        {
            sw.WriteLine($"[{utcTimestamp}] {logMessage}");
        }

        Console.WriteLine($"Log saved to {Path.GetFullPath(logPath)}");
    }
}
