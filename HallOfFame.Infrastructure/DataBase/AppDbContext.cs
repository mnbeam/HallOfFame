using System.Reflection;
using HallOfFame.Core.Entities;
using HallOfFame.Core.ExternalServices;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Infrastructure.DataBase;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; } = null!;

    public DbSet<Skill> Skills { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}