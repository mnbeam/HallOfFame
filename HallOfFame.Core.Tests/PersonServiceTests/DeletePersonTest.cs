using HallOfFame.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HallOfFame.Core.Tests.PersonServiceTests;

public class DeletePersonTest : PersonServiceTestsBase
{
    [Test]
    public async Task Delete__ShouldDeletePerson()
    {
        //arrange;
        var personId = (await DbContext.Persons.FirstAsync()).Id;

        //act
        await PersonService.Delete(personId);

        //assert
        Assert.Null(DbContext.Persons.SingleOrDefault(p => p.Id == personId));
    }

    [Test]
    public void Delete__ShouldThrowNotFoundException()
    {
        //arrange;
        var personId = 3;

        //act
        //assert
        Assert.ThrowsAsync<HallOfFameNotFoundException>(async () =>
            await PersonService.Delete(personId));
    }
}