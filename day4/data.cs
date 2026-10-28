using System;
using System.IO;
using System.Linq;

class DataPraser
{
    static void Main()
    {
        string path = "./video_games_list.csv";

        if (!File.Exists(path))
        {
            Console.WriteLine("File is not there.");
            return;
        }

        string[] games = File.ReadAllLines(path);

        Console.WriteLine("--- Game Summary ---");
        foreach (string line in games.Skip(1))
        {
            string[] columns = line.Split(',');
            if (columns.Length > 3)
            {
                string gameName = columns[0];
                string genre = columns[1]; 
                string releasedate = columns[2];
                string price = columns[3];
                Console.WriteLine($"{gameName} | {genre} | {releasedate} | {price}");
            }
        }

        var averagePrice = games.Skip(1)
                                .Select(x => decimal.TryParse(x.Split(',')[3], out var result) ? result : 0)
                                .Average();

        Console.WriteLine($"\nAverage Price: {averagePrice:C}");

        var gameCountByGenre = games.Skip(1)
                                    .GroupBy(x => x.Split(',')[1])
                                    .Select(g => new { Genre = g.Key, Count = g.Count() });
        
        Console.WriteLine("\n--- Game Count by Genre ---");
        foreach (var item in gameCountByGenre)
        {
            Console.WriteLine($"{item.Genre}: {item.Count}");
        }

        var mostRecent = games.Skip(1)
                              .Select(line =>
                              {
                                  var parts = line.Split(',');
                                  return new { Name = parts[0], ReleaseDate = DateTime.TryParse(parts[2], out var date) ? date : DateTime.MinValue };
                              })
                              .OrderByDescending(x => x.ReleaseDate)
                              .FirstOrDefault();
        
        if (mostRecent != null)
        {
            Console.WriteLine("\n--- Most Recent Game ---");
            Console.WriteLine($"{mostRecent.Name} ({mostRecent.ReleaseDate:yyyy-MM-dd})");
        }
    }
}