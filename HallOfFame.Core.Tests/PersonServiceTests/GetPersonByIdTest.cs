using HallOfFame.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HallOfFame.Core.Tests.PersonServiceTests;

[TestFixture]
public class GetPersonByIdTest : PersonServiceTestsBase
{
    [TestCase(1)]
    public async Task GetPersonById__ShouldGetPersonById(long personId)
    {
        //arrange;
        var expectedPerson = await DbContext.Persons.FirstOrDefaultAsync(p => p.Id == personId);

        //act
        var receivedPerson = await PersonService.GetPersonById(expectedPerson.Id);

        //assert
        Assert.AreEqual(expectedPerson.Name, receivedPerson.Name);
        Assert.AreEqual(expectedPerson.DisplayName, receivedPerson.DisplayName);

        for (var i = 0; i < expectedPerson.Skills.Count; i++)
        {
            Assert.AreEqual(expectedPerson.Skills[i].Name, receivedPerson.Skills[i].Name);
            Assert.AreEqual(expectedPerson.Skills[i].Level, receivedPerson.Skills[i].Level);
        }
    }

    [TestCase(3)]
    public void GetPersonById__ShouldThrowNotFoundException(long personId)
    {
        Assert.ThrowsAsync<HallOfFameNotFoundException>(async () =>
            await PersonService.GetPersonById(personId));
    }
}