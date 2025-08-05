namespace BrickBreakerLeaderboard;

/// <summary>
/// Data structure to store score entry information
/// </summary>
[Serializable]
public class ScoreEntry
{
    public string EntryName;
    public int Score;

    public ScoreEntry()
    {
        EntryName = "Unknown";
        Score = -1;
    }

    public bool Valid => Score >= 0;
}