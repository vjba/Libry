namespace Libry;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        builder.AddServices(config);

        using var factory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = factory.CreateLogger("Program");

        var app = builder.Build();

        app.ConfigureServices();

        app.Run();
    }
}
