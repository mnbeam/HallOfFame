using HallOfFame.Core.Exceptions;
using HallOfFame.Core.Services.PersonService.Dtos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HallOfFame.Core.Tests.PersonServiceTests;

public class UpdatePersonTest : PersonServiceTestsBase
{
    [TestCase("nName", "ndName", "nsName", 6, 5)]
    public async Task Update__ShouldUpdatePersonDataByPersonId(
        string newPersonName,
        string newDisplayName,
        string nameOfNewSkill,
        byte levelOfNewSkill,
        byte newLevelOfTrackingSkill)
    {
        //arrange;
        var personBeforeUpdating = await DbContext.Persons.Include(p => p.Skills).FirstAsync();

        var trackingSkill = await DbContext.Skills.FirstAsync();

        var skillToRemove = personBeforeUpdating.Skills.First(s => s.Id != trackingSkill.Id);

        var personDto = new PersonDto
        {
            Name = newPersonName,
            DisplayName = newDisplayName,
            Skills = new List<SkillDto>
            {
                new()
                {
                    Name = trackingSkill.Name,
                    Level = newLevelOfTrackingSkill
                },
                new()
                {
                    Name = nameOfNewSkill,
                    Level = levelOfNewSkill
                }
            }
        };

        //act
        await PersonService.Update(personBeforeUpdating.Id, personDto);

        //assert
        var person = await DbContext.Persons.Include(p => p.Skills)
            .FirstAsync(p => p.Id == personBeforeUpdating.Id);

        Assert.AreEqual(personDto.Name, person.Name);
        Assert.AreEqual(personDto.DisplayName, person.DisplayName);
        Assert.AreEqual(personDto.Skills.Count, person.Skills!.Count);

        Assert.IsNull(person.Skills.FirstOrDefault(s => s.Name == skillToRemove.Name));
        Assert.AreEqual(personDto.Skills.First(s => s.Name == trackingSkill.Name).Level,
            person.Skills.First(s => s.Name == trackingSkill.Name).Level);
        Assert.AreEqual(personDto.Skills.First(s => s.Name == nameOfNewSkill).Level,
            person.Skills.First(s => s.Name != trackingSkill.Name).Level);
    }

    [Test]
    public async Task UpdatingPersonTest__FailOnInvalidInputData()
    {
        //arrange;
        var personId = (await DbContext.Persons.FirstAsync()).Id;

        var personDto = new PersonDto
        {
            Name = "",
            DisplayName = "",
            Skills = new List<SkillDto>
            {
                new()
                {
                    Name = "",
                    Level = 124
                }
            }
        };

        //act
        //assert
        Assert.ThrowsAsync<HallOfFameValidationException>(async () =>
            await PersonService.Update(personId, personDto));
    }
}