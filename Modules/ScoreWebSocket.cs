using EmbedIO.WebSockets;
using Newtonsoft.Json;

namespace BrickBreakerLeaderboard;

/// <summary>
/// This websocket broadcasts when a score is added to the leaderboard, this can be used
/// to refresh client to display latest information.
/// </summary>
public class ScoreWebSocket : WebSocketModule
{
    public ScoreWebSocket(string urlPath) : base(urlPath, true)
    {
        InstanceRegistry.Register(this);
    }

    protected override Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
    {
        return Task.CompletedTask;
    }

    public static void BroadcastLeaderboardUpdate()
    {
        var data = JsonConvert.SerializeObject(ScoreStorage.GetTopScores());
        InstanceRegistry.Get<ScoreWebSocket>()?.BroadcastAsync(data);
        Console.WriteLine("Leaderboard Update BroadCasted");
    }
}