using EmbedIO.WebSockets;
using Newtonsoft.Json;

namespace BrickBreakerLeaderboard;

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