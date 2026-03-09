using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Morpion;
using Morpion.Infrastructure.Persistance;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));
        
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddTransient<GameManager>();
    });


var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    try
    {
        var canConnect = dbContext.Database.CanConnect();
        Console.WriteLine(canConnect 
            ? "Connexion à la base de données réussie" 
            : "Échec de la connexion à la base de données");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur de connexion : {ex.Message}");
        return;
    }
}

var game = host.Services.GetRequiredService<GameManager>();

await game.StartGame();

Console.ReadLine();