using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HallOfFame.Core.Tests.PersonServiceTests;

[TestFixture]
public class GetAllPersonsTests : PersonServiceTestsBase
{
    [Test]
    public async Task GetAll__ShouldGetAllPersons()
    {
        //arrange;
        var expectedPersons = await DbContext.Persons.Include(p => p.Skills).ToListAsync();

        //act
        var receivedPersons = await PersonService.GetAll();

        //assert
        for (var i = 0; i < expectedPersons.Count; i++)
        {
            Assert.AreEqual(expectedPersons[i].Id, receivedPersons[i].Id);
            Assert.AreEqual(expectedPersons[i].Name, receivedPersons[i].Name);
            Assert.AreEqual(expectedPersons[i].DisplayName, receivedPersons[i].DisplayName);

            for (var j = 0; j < expectedPersons[i].Skills.Count; j++)
            {
                Assert.AreEqual(expectedPersons[i].Skills[j].Name,
                    receivedPersons[i].Skills[j].Name);
                Assert.AreEqual(expectedPersons[i].Skills[j].Level,
                    receivedPersons[i].Skills[j].Level);
            }
        }
    }
}