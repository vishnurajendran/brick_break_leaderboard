
using BrickBreakerLeaderboard;
using EmbedIO;
using EmbedIO.WebApi;
using EmbedIO.WebSockets;

public class Program
{
    public static void Main(String[] args)
    {
        // create a server instance and run
        var server = CreateServer("http://*:9000");
        server.RunAsync();
        Console.WriteLine("Server Started at Port 9000");
        Console.WriteLine("Press any Key to Close server");
        
        // Block the main thread from ending execution
        Console.ReadKey();
        server.Dispose();
        Console.WriteLine("Server closed");
    }

    private static WebServer CreateServer(string url)
    {
        return new WebServer(o => o
            .WithUrlPrefix(url)
            .WithMode(HttpListenerMode.EmbedIO))
            .WithLocalSessionManager()
            .WithModule(new ScoreWebSocket("/ws"))
            .WithModule(new RequestLogger())
            .WithWebApi("/api", m => m.WithController<LeaderboardController>()
            .HandleUnhandledException(async (ctx, ex) =>
                {
                    Console.WriteLine($"Exception: {ex}");
                    throw HttpException.BadRequest();
                }))
           .HandleUnhandledException(async (ctx, httpEx) => 
                {
                    await ctx.SendDataAsync(new { error = httpEx.Message });
                })
            .WithStaticFolder("/", "./Public", true);
    }
}