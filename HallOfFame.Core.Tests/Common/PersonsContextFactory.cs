using HallOfFame.Core.Entities;
using HallOfFame.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Core.Tests.Common;

public class PersonsContextFactory
{
    public static Person Person1 => new()
    {
        Name = "FirstPerson",
        DisplayName = "First",
        Skills = new List<Skill>
        {
            new()
            {
                Name = "FirstSkill",
                Level = 1
            },
            new()
            {
                Name = "SecondSkill",
                Level = 2
            }
        }
    };

    public static Person Person2 => new()
    {
        Name = "SecondPerson",
        DisplayName = "Second",
        Skills = new List<Skill>
        {
            new()
            {
                Name = "FirstSkill",
                Level = 1
            },
            new()
            {
                Name = "SecondSkill",
                Level = 5
            }
        }
    };

    public static async Task<AppDbContext> Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();

        context.Persons.AddRange(
            Person1,
            Person2
        );
        await context.SaveChangesAsync();
        return context;
    }

    public static async Task Destroy(AppDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.DisposeAsync();
    }
}