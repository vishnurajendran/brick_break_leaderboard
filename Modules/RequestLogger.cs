using EmbedIO;

namespace BrickBreakerLeaderboard;

/// <summary>
/// A simple class to handle logging incoming request to the server
/// </summary>
public class RequestLogger : WebModuleBase
{
    public RequestLogger(string baseRoute = "/") : base(baseRoute)
    {
    }

    protected override async Task OnRequestAsync(IHttpContext context)
    {
        Console.WriteLine($"[{DateTime.Now}] {context.Request.HttpMethod} {context.RequestedPath}");
    }

    public override bool IsFinalHandler => false;
}