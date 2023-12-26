using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data;

public class StreamerDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Streamer;Trusted_Connection=True;")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Streamer>()
            .HasMany(m => m.Videos)
            .WithOne(m => m.Streamer)
            .HasForeignKey(m => m.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Video>()
            .HasMany(m => m.Actors)
            .WithMany(m => m.Videos)
            .UsingEntity<VideoActor>(
            pt => pt.HasKey(e => new {e.ActorId, e.VideoId})
            );
            
    }
    public DbSet<Streamer> Streamers { get; set; }
    public DbSet<Video> Videos { get; set; }
}
