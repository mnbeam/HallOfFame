using HallOfFame.Core.Services.PersonService;
using HallOfFame.Core.Services.PersonService.Validators;
using HallOfFame.Core.Tests.Common;
using HallOfFame.Infrastructure.DataBase;
using NUnit.Framework;

namespace HallOfFame.Core.Tests.PersonServiceTests;

[TestFixture]
public abstract class PersonServiceTestsBase
{
    [SetUp]
    public async Task Setup()
    {
        DbContext = await PersonsContextFactory.Create();
        PersonService = new PersonService(DbContext, new PersonDtoValidator());
    }

    [TearDown]
    public async Task Destroy()
    {
        await PersonsContextFactory.Destroy(DbContext);
    }

    protected AppDbContext DbContext = null!;

    protected PersonService PersonService = null!;
}