using System.Text;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Newtonsoft.Json;

namespace BrickBreakerLeaderboard;

public class LeaderboardController : WebApiController
{
    [Route(HttpVerbs.Post, "/submit")]
    public async Task SubmitScore()
    {
        using var reader = new StreamReader(HttpContext.OpenRequestStream());
        var dataBody = await reader.ReadToEndAsync();
        if (string.IsNullOrEmpty(dataBody))
            throw new HttpRequestException();
        
        Console.WriteLine($"Body: {dataBody}");
        var data = JsonConvert.DeserializeObject<ScoreEntry>(dataBody);
        if (data == null || !data.Valid)
        {
            throw new HttpRequestException();
        }
        
        ScoreStorage.AddScore(data);
        await HttpContext.SendDataAsync(new { success = true });
        ScoreWebSocket.BroadcastLeaderboardUpdate();
    }

    [Route(HttpVerbs.Get, "/leaderboard")]
    public async Task GetLeaderboard()
    {
        var scores = ScoreStorage.GetTopScores(10);
        var json = JsonConvert.SerializeObject(scores);
        Console.WriteLine(json);
        await HttpContext.SendStringAsync(json, "json", Encoding.Default);
    }
}