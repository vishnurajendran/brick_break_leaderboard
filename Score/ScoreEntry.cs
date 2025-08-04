namespace BrickBreakerLeaderboard;

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