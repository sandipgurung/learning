using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

class TaskManager
{
    private const string FilePath = "tasks.json";

    static void Main()
    {
        List<TaskItem> tasks = LoadTasks();

        Console.WriteLine($"Loaded {tasks.Count} tasks from file.");

        while (true)
        {
            Console.Write("\nEnter a new task title (or type 'exit' to quit): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit")
            {
                break;
            }

            bool duplicateExists = tasks.Any(t => t.Title.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (duplicateExists)
            {
                Console.WriteLine("Warning: A task with that title already exists!");
                continue;
            }

            tasks.Add(new TaskItem
            {
                Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1,
                Title = input,
                IsCompleted = false
            });

            SaveTasks(tasks);
            Console.WriteLine($"Successfully saved! Total tasks: {tasks.Count}");
        }

        int completedCount = tasks.Count(t => t.IsCompleted);
        Console.WriteLine($"\nExiting. Summary: {completedCount} completed, {tasks.Count - completedCount} pending.");
    }

    public static List<TaskItem> LoadTasks()
    {
        if (!File.Exists(FilePath))
        {
            return new List<TaskItem>();
        }
        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
    }  

    public static void SaveTasks(List<TaskItem> tasks)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tasks, options);
        File.WriteAllText(FilePath, json);
    }
}