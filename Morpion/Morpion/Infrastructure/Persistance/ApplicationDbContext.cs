using Microsoft.EntityFrameworkCore;
using Morpion.Domain.Entities;

namespace Morpion.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}