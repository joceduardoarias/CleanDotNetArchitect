using CleanArchitecture.Identity.Configurations;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity;

public class CleanArchitectureDbContext : IdentityDbContext<ApplicationUser>
{
    public CleanArchitectureDbContext(DbContextOptions<CleanArchitectureDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.ApplyConfiguration(new RoleConfiguration());
        //builder.ApplyConfiguration(new UserConfiguration()); //TODO
        //builder.ApplyConfiguration(new UserRoleConfiguration());

    }
}
