using InternshipEntryTask.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipEntryTask.Infrastructure.Data;

public class GameDbContext(DbContextOptions<GameDbContext> options) : DbContext(options)
{
    public DbSet<TicTacToeGame> Games { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicTacToeGame>().HasKey(g => g.Id);
    }
}