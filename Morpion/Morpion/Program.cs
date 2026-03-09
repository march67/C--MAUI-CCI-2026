using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Morpion;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddTransient<Game>();
    });

var host = builder.Build();

var game = host.Services.GetRequiredService<Game>();

await game.StartGame();

Console.ReadLine();