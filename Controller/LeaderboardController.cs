using System.Text;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Newtonsoft.Json;

namespace BrickBreakerLeaderboard;

/// <summary>
/// This class handles the API routes and its functionalities for the leaderboard.
/// </summary>
public class LeaderboardController : WebApiController
{
    [Route(HttpVerbs.Post, "/submit-score")]
    public async Task SubmitScore()
    {
        // was having issues using the default json deserializer, so bypassing it 
        // and using Newtonsoft.Json to parse it manually.
        
        using var reader = new StreamReader(HttpContext.OpenRequestStream());
        var dataBody = await reader.ReadToEndAsync();
        if (string.IsNullOrEmpty(dataBody))
            throw new HttpRequestException();
        
        //Log the body to the console, for debugging purposes.
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

    [Route(HttpVerbs.Get, "/get-leaderboard")]
    public async Task GetLeaderboard()
    {
        var scores = ScoreStorage.GetTopScores(10);
        //Serialise the result and send it across
        var json = JsonConvert.SerializeObject(scores);
        Console.WriteLine(json);
        await HttpContext.SendStringAsync(json, "json", Encoding.Default);
    }
}