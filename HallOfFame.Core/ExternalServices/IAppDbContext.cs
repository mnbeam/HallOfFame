using HallOfFame.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Core.ExternalServices;

public interface IAppDbContext
{
    public DbSet<Person> Persons { get; set; }

    public DbSet<Skill> Skills { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}