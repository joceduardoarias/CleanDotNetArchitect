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
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Streamer;Trusted_Connection=True;");
    }
    public DbSet<Streamer> Streamers { get; set; }
    public DbSet<Video> Videos { get; set; }
}
