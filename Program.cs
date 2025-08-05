
using BrickBreakerLeaderboard;
using EmbedIO;
using EmbedIO.WebApi;
using EmbedIO.WebSockets;

public class Program
{
    public static void Main(String[] args)
    {
        // create a server instance and run
        // I have taken 9000 as the port, As far as I'm aware this port is always available in Mac, Linux and Windows.
        
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
        // I hate builder patterns lol, the build call is way too big and honestly 
        // ugly looking :p.
        return new WebServer(o => o
            .WithUrlPrefix(url)
            .WithMode(HttpListenerMode.EmbedIO))
            .WithLocalSessionManager()
            .WithModule(new ScoreWebSocket("/ws"))
            .WithModule(new RequestLogger())
            .WithWebApi("/api", m => m.WithController<LeaderboardController>()
            .HandleUnhandledException((ctx, ex) =>
                {
                    Console.WriteLine($"Exception: {ex}");
                    throw HttpException.BadRequest();
                })) //Error handler for Controller
           .HandleUnhandledException(async (ctx, httpEx) => 
                {
                    await ctx.SendDataAsync(new { error = httpEx.Message });
                }) //Error Handler for WebServer
            .WithStaticFolder("/", "./Public", true);
    }
}