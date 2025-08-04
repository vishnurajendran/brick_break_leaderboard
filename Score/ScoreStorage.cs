using System.Xml.Linq;
using Newtonsoft.Json;

namespace BrickBreakerLeaderboard;

public static class ScoreStorage
{
    private static readonly string FilePath = "Scores.json";
    private static List<ScoreEntry> _scores = LoadScores();
    
    public static void AddScore(ScoreEntry entry)
    {
        _scores.Add(entry);
        Console.WriteLine("Score Added");
        Save();
    }

    public static List<ScoreEntry> GetTopScores(int limit = 10)
    {
        return _scores.OrderByDescending(s => s.Score).Take(limit).ToList();
    }

    private static List<ScoreEntry> LoadScores()
    {
        if (!File.Exists(FilePath))
            return new List<ScoreEntry>();

        var data = JsonConvert.DeserializeObject<List<ScoreEntry>>(File.ReadAllText(FilePath))
                   ?? new List<ScoreEntry>();
        
        //a cleanup stage
        data.RemoveAll(a => a == null || !a.Valid);
        return data;
    }

    private static void Save()
    {
        _scores.RemoveAll(a => a == null || !a.Valid);
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(_scores));
        Console.WriteLine("Updated Score Saved to Disk");
    }
}